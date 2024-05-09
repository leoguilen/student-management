namespace StudentManagement.Api.IntegrationTest;

public abstract class IntegrationTest : IClassFixture<CustomWebApplicationFactory>
{
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

    private static async Task<string> GetAccessTokenAsync()
    {
        return await Task.FromResult("jwt");
    }
}
