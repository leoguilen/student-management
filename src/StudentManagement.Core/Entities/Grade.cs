namespace StudentManagement.Core.Entities;

public class Grade
{
    public Guid Id { get; init; }
    
    public Guid StudentId { get; init; }
    
    public Guid SubjectId { get; init; }
    
    public int Value { get; init; }

    public Student? Student { get; init; }

    public Subject? Subject { get; init; }
}
