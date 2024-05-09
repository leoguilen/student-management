namespace StudentManagement.Api.IntegrationTest.Endpoints;

[Trait("Category", "Integration")]
public class StudentsEndpointsTest(
    CustomWebApplicationFactory factory,
    ITestOutputHelper outputHelper)
    : IntegrationTest(factory, outputHelper)
{
}
