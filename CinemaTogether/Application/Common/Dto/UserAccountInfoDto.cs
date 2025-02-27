using Domain.Enums;

namespace Application.Common.Dto;

public record UserAccountInfoDto(
    Guid Id,
    string Email,
    string PasswordHash,
    Role Role);