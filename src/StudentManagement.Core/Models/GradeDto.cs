namespace StudentManagement.Core.Models;

public record GradeDto
{
    public Guid Id { get; init; }
    
    public Guid StudentId { get; init; }

    public StudentDto? Student { get; init; } = null;
    
    public Guid SubjectId { get; init; }

    public SubjectDto? Subject { get; init; } = null;
    
    public int Value { get; init; }

    public static implicit operator GradeDto(Grade grade) => new()
    {
        Id = grade.Id,
        StudentId = grade.StudentId,
        SubjectId = grade.SubjectId,
        Value = grade.Value,
        Student = grade.Student is not null ? (StudentDto)grade.Student : null,
        Subject = grade.Subject is not null ? (SubjectDto)grade.Subject : null,
    };

    public static implicit operator Grade(GradeDto grade) => new()
    {
        Id = grade.Id,
        StudentId = grade.StudentId,
        SubjectId = grade.SubjectId,
        Value = grade.Value,
        Student = grade.Student is not null ? (Student)grade.Student : null,
        Subject = grade.Subject is not null ? (Subject)grade.Subject : null,
    };
}
