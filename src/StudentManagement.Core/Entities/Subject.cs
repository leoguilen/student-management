namespace StudentManagement.Core.Entities;

public class Subject
{
    public Guid Id { get; init; }
    
    public required string Name { get; init; }
    
    public List<Grade> Grades { get; init; } = [];
}
