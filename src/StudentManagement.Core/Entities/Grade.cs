namespace StudentManagement.Core.Entities;

public class Grade
{
    public Guid Id { get; set; }
    
    public Guid StudentId { get; set; }
    
    public Guid SubjectId { get; set; }
    
    public int Value { get; set; }

    public Student? Student { get; set; }

    public Subject? Subject { get; set; }

    internal void Update(GradeDto grade)
    {
        Value = grade.Value;
    }
}
