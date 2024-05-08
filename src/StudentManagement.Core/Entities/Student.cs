namespace StudentManagement.Core.Entities;

public class Student
{
    public Guid Id { get; init; }
    
    public required string Name { get; init; }
    
    public required DateTime DateOfBirth { get; init; }
    
    public required string Cpf { get; init; }
    
    public required string Address { get; init; }
    
    public required string PhoneNumber { get; init; }

    public List<Grade> Grades { get; init; } = [];
}
