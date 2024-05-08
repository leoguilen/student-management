namespace StudentManagement.Api.Services;

public interface ITokenService
{
    (string Token, DateTime Expiry) Generate(Student? student);
}
