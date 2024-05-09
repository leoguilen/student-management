namespace StudentManagement.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDefaultServices(this IServiceCollection services)
    {
        return services
            .AddControllers()
            .Services
            .AddEndpointsApiExplorer()
            .AddProblemDetails()
            .AddHttpContextAccessor()
            .AddMemoryCache()
            .AddExceptionHandler<GlobalExceptionHandlerMiddleware>();
    }

    public static IServiceCollection AddOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .Configure<JwtOptions>(configuration.GetRequiredSection("Authentication:Jwt"));
    }

    public static IServiceCollection AddAuthServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddAuthorization(options => {
                options.AddPolicy("StudentIdPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("Student");
                    policy.Requirements.Add(new StudentIdRequirement());
                });
            })
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var jwtOptions = configuration.GetRequiredSection("Authentication:Jwt");
                    var issuerSigningKeys = jwtOptions
                        .GetRequiredSection("IssuerSigningKeys").Get<string[]>()!
                        .Select(key => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)))
                        .ToArray();
                    
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuers = jwtOptions.GetRequiredSection("ValidIssuers").Get<string[]>(),
                        ValidateAudience = true,
                        ValidAudiences = jwtOptions.GetRequiredSection("ValidAudiences").Get<string[]>(),
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKeys = issuerSigningKeys,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                    };
                })
                .Services
            .AddScoped<IAuthorizationHandler, StudentIdHandler>()
            .AddSingleton<ITokenService, TokenService>();
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "StudentManagement.Api",
                Description = "Student Management API",
                Version = "v1",
            });
            
            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization : Bearer {token}\"",
                Type = SecuritySchemeType.Http,
                In = ParameterLocation.Header,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme,
                        },
                    }, []
                }
            });

            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "StudentManagement.Api.xml"));
        });

        return services;
    }
}
