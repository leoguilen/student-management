namespace StudentManagement.Api.IntegrationTest.Endpoints;

[Trait("Category", "Integration")]
public class SubjectsEndpointsTest(
    CustomWebApplicationFactory factory,
    ITestOutputHelper outputHelper)
    : IntegrationTest(factory, outputHelper)
{
    [Fact]
    public void Test()
    {
        Assert.True(true);
    }
}
