namespace StudentManagement.Core.Models;

public record SubjectDto
{
    public Guid Id { get; init; }

    public required string Name { get; init; }

    public static implicit operator SubjectDto(Subject subject) => new()
    {
        Id = subject.Id,
        Name = subject.Name
    };

    public static implicit operator Subject(SubjectDto subject) => new()
    {
        Id = subject.Id,
        Name = subject.Name
    };
}
