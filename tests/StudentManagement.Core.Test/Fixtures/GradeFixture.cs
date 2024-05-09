namespace StudentManagement.Core.Test.Fixtures;

internal class GradeFixture : Faker<Grade>
{
    public GradeFixture()
    {
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.StudentId, f => f.Random.Guid());
        RuleFor(x => x.SubjectId, f => f.Random.Guid());
        RuleFor(x => x.Value, f => f.Random.Number(0, 10));
    }
}