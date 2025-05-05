namespace Application.Common.Dto;

public record UserPublicInfoDto(
    Guid UserId,
    string Username,
    string Name,
    DateTime? DateOfBirth,
    string Gender,
    string ProfilePicturePath,
    string CityName,
    string CountryName);