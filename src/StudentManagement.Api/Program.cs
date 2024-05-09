var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultServices();
builder.Services.AddAuthServices(builder.Configuration);
builder.Services.AddOptions(builder.Configuration);
builder.Services.AddCore();
builder.Services.AddInfrastructure();
builder.Services.AddSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthentication();
app.MapControllers();

await app.RunAsync();

public partial class Program { }
