namespace Application.Common.Dto;

public record UserPrivateInfoDto(
    string Email,
    string Username,
    bool TwoFactorEnabled,
    bool IsAdmin,
    string Name,
    DateTime? DateOfBirth,
    string Gender,
    string ProfilePicturePath,
    string CityName,
    string CountryName);