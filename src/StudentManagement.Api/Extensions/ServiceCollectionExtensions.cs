namespace StudentManagement.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDefaultServices(this IServiceCollection services)
    {
        return services
            .AddControllers()
            .Services
            .AddEndpointsApiExplorer()
            .AddHttpContextAccessor()
            .AddMemoryCache();
    }

    public static IServiceCollection AddAuthServices(this IServiceCollection services)
    {
        return services
            .AddAuthorization()
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer()
                .Services;
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
