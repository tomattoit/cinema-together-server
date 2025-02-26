using Domain.Enums;

namespace Application.Common.Dto;

public record UpdateUserInfoDto(
    string Email,
    string Username,
    string Name,
    DateTime? DateOfBirth,
    Gender? Gender,
    string ProfilePicturePath,
    Guid CityId);