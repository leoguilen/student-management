namespace StudentManagement.Api.IntegrationTest;

public abstract class IntegrationTest(
    CustomWebApplicationFactory factory,
    ITestOutputHelper outputHelper)
    : IClassFixture<CustomWebApplicationFactory>
{
    private static string _accessToken = string.Empty;

    protected readonly StudentFixture StudentFixture = new();
    
    protected readonly StudentRequestFixture StudentRequestFixture = new();

    protected CustomWebApplicationFactory Factory { get; } = factory;

    protected HttpClient Client { get; } = factory.CreateClient();

    protected ITestOutputHelper Logger { get; } = outputHelper;

    protected async Task AuthenticateAsync(Guid? studentId = null)
    {
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessTokenAsync(Client, studentId));
    }

    protected async Task<Guid> AddStudentInDatabaseAsync()
    {
        using var scope = Factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var entry = await context.Students.AddAsync(StudentFixture.Generate());
        await context.SaveChangesAsync();
        return entry.Entity.Id;
    }

    private static async Task<string> GetAccessTokenAsync(HttpClient client, Guid? studentId = null)
    {
        if (!string.IsNullOrEmpty(_accessToken) && !studentId.HasValue)
        {
            return _accessToken;
        }

        var request = studentId.HasValue ? new AuthRequest { StudentId = studentId } : null;
        var response = await client.PostAsJsonAsync("/api/auth/token", request);
        var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
        _accessToken = authResponse!.AccessToken;
        return _accessToken;
    }
}
