namespace StudentManagement.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseInMemoryDatabase("StudentManagementMemoryDatabase");
        });

        services.AddScoped<IStudentRepository, StudentRepository>();
        // services.AddScoped<ISubjectRepository, SubjectRepository>();
        // services.AddScoped<IGradeRepository, GradeRepository>();
        
        return services;
    }
}
