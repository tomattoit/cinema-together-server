namespace Application.Common.Dto;

public record MovieListItem(Guid Id,
    string Title,
    DateTime ReleaseDate,
    string PosterPath,
    decimal RatingTmdb);