﻿namespace StudentManagement.Api.Contracts.Responses;

/// <summary>
/// Represents a student response schema.
/// </summary>
public record StudentResponse
{
    /// <summary>
    /// The ID of the student.
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    public required Guid Id { get; init; }
    
    /// <summary>
    /// The name of the student.
    /// </summary>
    /// <example>John Doe</example>
    public required string Name { get; init; }

    /// <summary>
    /// The date of birth of the student.
    /// </summary>
    /// <example>2000-01-01</example>
    [DataType(DataType.Date)]
    public required DateTime DateOfBirth { get; init; }
    
    /// <summary>
    /// The CPF of the student.
    /// </summary>
    /// <example>123.456.789-00</example>
    public required string Cpf { get; init; }
    
    /// <summary>
    /// The address of the student.
    /// </summary>
    /// <example>123 Main St, Springfield, IL 62701</example>
    public required string Address { get; init; }
    
    /// <summary>
    /// The phone number of the student.
    /// </summary>
    /// <example>(55) 5555-5555</example>
    public required string PhoneNumber { get; init; }

    public static StudentResponse From(StudentDto student) => new()
    {
        Id = student.Id,
        Name = student.Name,
        DateOfBirth = student.DateOfBirth.ToDateTime(TimeOnly.MinValue),
        Cpf = student.Cpf,
        Address = student.Address,
        PhoneNumber = student.PhoneNumber,
    };
}
