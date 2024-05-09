namespace StudentManagement.Core.Test.Fixtures;

internal class GradeDtoFixture : Faker<GradeDto>
{
    public GradeDtoFixture()
    {
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.StudentId, f => f.Random.Guid());
        RuleFor(x => x.SubjectId, f => f.Random.Guid());
        RuleFor(x => x.Value, f => f.Random.Int(0, 10));
    }
}
