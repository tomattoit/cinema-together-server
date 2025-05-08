namespace Application.Common.Dto;

public record MovieReviewDto(MovieListItem Movie, decimal Rate, string Comment, Guid UserId, string Username, string ProfileImagePath);