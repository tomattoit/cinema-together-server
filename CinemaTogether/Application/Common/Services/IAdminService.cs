using Application.Common.Dto;

namespace Application.Common.Services;

public interface IAdminService
{
    Task UpdateMovieAsync(MovieUpdateDto movieDto, CancellationToken cancellationToken);
    Task DeleteMovieAsync(Guid movieId, CancellationToken cancellationToken);
    Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken);
    Task DeleteGroupAsync(Guid groupId, CancellationToken cancellationToken);
}