namespace StudentManagement.Core.Test.Fixtures;

internal class StudentDtoFixture : Faker<StudentDto>
{
    public StudentDtoFixture()
    {
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.Name, f => f.Person.FirstName);
        RuleFor(x => x.DateOfBirth, f => DateOnly.FromDateTime(f.Person.DateOfBirth));
        RuleFor(x => x.Cpf, f => f.Random.String2(11, "0123456789"));
        RuleFor(x => x.PhoneNumber, f => f.Person.Phone);
        RuleFor(x => x.Address, f => f.Address.FullAddress());
    }
}
