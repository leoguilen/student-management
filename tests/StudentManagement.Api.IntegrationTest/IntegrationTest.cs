namespace StudentManagement.Api.IntegrationTest;

public abstract class IntegrationTest : IClassFixture<CustomWebApplicationFactory>
{
    protected static readonly StudentFixture StudentFixture = new();

    protected CustomWebApplicationFactory Factory { get; }
    
    protected HttpClient Client { get; }

    protected ITestOutputHelper Logger { get; }

    protected IntegrationTest(
        CustomWebApplicationFactory factory,
        ITestOutputHelper outputHelper)
    {
        Factory = factory;
        Client = factory.CreateClient();
        Logger = outputHelper;
    }

    protected async Task AuthenticateAsync()
    {
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessTokenAsync());
    }

    protected async Task<Guid> AddStudentInDatabaseAsync()
    {
        using var scope = Factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var entry = await context.Students.AddAsync(StudentFixture.Generate());
        await context.SaveChangesAsync();
        return entry.Entity.Id;
    }

    private static async Task<string> GetAccessTokenAsync()
    {
        return await Task.FromResult("jwt");
    }
}
