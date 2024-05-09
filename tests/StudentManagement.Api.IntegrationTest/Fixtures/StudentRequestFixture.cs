namespace StudentManagement.Api.IntegrationTest.Fixtures;

public class StudentRequestFixture : Faker<StudentRequest>
{
    public StudentRequestFixture()
    {
        RuleFor(x => x.Name, f => f.Person.FirstName);
        RuleFor(x => x.DateOfBirth, f => f.Date.Recent().Date);
        RuleFor(x => x.Cpf, f => f.Random.String2(11, "0123456789"));
        RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("(##) ####-####"));
        RuleFor(x => x.Address, f => f.Address.StreetAddress());
    }
}
