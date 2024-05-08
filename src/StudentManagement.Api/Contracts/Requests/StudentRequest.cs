namespace StudentManagement.Api.Contracts.Requests;

/// <summary>
/// Represents the request to register or update a student.
/// </summary>
public record StudentRequest
{   
    /// <summary>
    /// The name of the student.
    /// </summary>
    /// <example>John Doe</example>
    [Required, MaxLength(100)]
    public required string Name { get; init; }

    /// <summary>
    /// The date of birth of the student.
    /// </summary>
    /// <example>2000-01-01</example>
    [Required, DataType(DataType.Date)]
    public required DateTime DateOfBirth { get; init; }
    
    /// <summary>
    /// The CPF of the student.
    /// </summary>
    /// <example>123.456.789-00</example>
    [Required, Length(11, 14)]
    public required string Cpf { get; init; }
    
    /// <summary>
    /// The address of the student.
    /// </summary>
    /// <example>123 Main St, Springfield, IL 62701</example>
    [Required, MaxLength(200)]
    public required string Address { get; init; }
    
    /// <summary>
    /// The phone number of the student.
    /// </summary>
    /// <example>(55) 5555-5555</example>
    [Required, MaxLength(20)]
    public required string PhoneNumber { get; init; }

    public static implicit operator StudentDto(StudentRequest request) => new()
    {
        Name = request.Name,
        DateOfBirth = DateOnly.FromDateTime(request.DateOfBirth),
        Cpf = request.Cpf,
        Address = request.Address,
        PhoneNumber = request.PhoneNumber
    };
}
