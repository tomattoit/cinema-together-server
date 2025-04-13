namespace Application.Common.Dto;

public record UserPublicInfoDto(
    string Username,
    string Name,
    DateTime? DateOfBirth,
    string Gender,
    string ProfilePicturePath,
    string CityName,
    string CountryName);