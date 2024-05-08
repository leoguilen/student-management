namespace StudentManagement.Core.Models;

public record StudentFilter(string? Name, DateOnly? DateOfBirth, string? Cpf);
