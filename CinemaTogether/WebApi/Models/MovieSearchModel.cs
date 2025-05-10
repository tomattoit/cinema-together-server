namespace WebApi.Models;

public record MovieSearchModel(
    string? Title,
    List<Guid>? GenreIds,
    DateTime? ReleaseDateFrom,
    DateTime? ReleaseDateTo,
    int Page = 1,
    int PageSize = 10);