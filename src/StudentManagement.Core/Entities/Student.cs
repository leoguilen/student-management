namespace StudentManagement.Core.Entities;

public class Student
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Cpf { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public ICollection<Grade> Grades { get; set; } = [];

    public void Update(StudentDto updatedStudent)
    {
        Name = updatedStudent.Name;
        DateOfBirth = updatedStudent.DateOfBirth;
        Cpf = updatedStudent.Cpf;
        Address = updatedStudent.Address;
        PhoneNumber = updatedStudent.PhoneNumber;
    }
}
