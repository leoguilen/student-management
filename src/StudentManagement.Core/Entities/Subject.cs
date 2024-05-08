namespace StudentManagement.Core.Entities;

public class Subject
{
    public Guid Id { get; init; }
    
    public required string Name { get; init; }
    
    public ICollection<Grade> Grades { get; init; } = [];
}
