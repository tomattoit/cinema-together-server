namespace Application.Common.Dto;

public record MovieDto(
    Guid Id,
    string Title,
    int Duration,
    List<string> Genres,
    string Description,
    string Director,
    string Actors,
    DateTime ReleaseDate,
    string PosterPath,
    decimal RatingTmdb,
    decimal Rating,
    int RatingCount);