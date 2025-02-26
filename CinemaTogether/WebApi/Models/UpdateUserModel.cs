using Domain.Enums;

namespace WebApi.Models;

public record UpdateUserModel(
    string Email,
    string Username,
    string Name,
    DateTime? DateOfBirth,
    Gender? Gender,
    string ProfilePicturePath,
    Guid CityId);