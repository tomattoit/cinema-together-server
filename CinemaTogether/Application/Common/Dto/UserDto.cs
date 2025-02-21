namespace Application.Common.Dto;

public record UserDto(
    Guid Id,
    string Email,
    string PasswordHash,
    string RoleName);