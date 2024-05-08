namespace StudentManagement.Core.Models;

public record StudentDto
{
    public Guid Id { get; init; }

    public required string Name { get; init; }

    public required DateOnly DateOfBirth { get; init; }

    public required string Cpf { get; init; }

    public required string Address { get; init; }

    public required string PhoneNumber { get; init; }

    public static implicit operator StudentDto(Student student) => new()
    {
        Id = student.Id,
        Name = student.Name,
        DateOfBirth = student.DateOfBirth,
        Cpf = student.Cpf,
        Address = student.Address,
        PhoneNumber = student.PhoneNumber
    };

    public static implicit operator Student(StudentDto student) => new()
    {
        Id = student.Id,
        Name = student.Name,
        DateOfBirth = student.DateOfBirth,
        Cpf = student.Cpf,
        Address = student.Address,
        PhoneNumber = student.PhoneNumber
    };
}
