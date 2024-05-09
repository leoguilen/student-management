namespace StudentManagement.Core.Entities;

public class Subject
{
    public Guid Id { get; set; }
    
    public required string Name { get; set; }
    
    public ICollection<Grade> Grades { get; set; } = [];

    internal void Update(SubjectDto subject)
    {
        Name = subject.Name;
    }
}
