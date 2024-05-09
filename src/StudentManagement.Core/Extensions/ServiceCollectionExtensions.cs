namespace StudentManagement.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services
            .AddScoped<IStudentService, StudentService>()
            .AddScoped<ISubjectService, SubjectService>()
            .AddScoped<IGradeService, GradeService>();
    }
}
