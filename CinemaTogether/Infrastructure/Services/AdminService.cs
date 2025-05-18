using Application.Common.Dto;
using Application.Common.Services;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AdminService(ApplicationDbContext context) : IAdminService
{
    public async Task UpdateMovieAsync(MovieUpdateDto movieDto, CancellationToken cancellationToken = default)
    {
        var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == movieDto.Id, cancellationToken);
        
        if (movie == null) throw new NotFoundException("Movie", "Id", movieDto.Id.ToString());

        var newGenres = context.Genres.Where(g => movieDto.Genres.Contains(g.Name));
        
        var movieGenres = context.MovieGenres.Where(mg => mg.MovieId == movieDto.Id);
        context.MovieGenres.RemoveRange(movieGenres);

        foreach (var genre in newGenres)
        {
            await context.AddAsync(new MovieGenre
            {
                Genre = genre,
                GenreId = genre.Id,
                Movie = movie,
                MovieId = movie.Id
            });
        }
        
        movie.Title = movieDto.Title;
        movie.ReleaseDate = movieDto.ReleaseDate;
        movie.Director = movieDto.Director;
        movie.Actors = movieDto.Actors;
        movie.Description = movieDto.Description;
        movie.PosterPath = movieDto.PosterPath;
        
        context.Movies.Update(movie);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteMovieAsync(Guid movieId, CancellationToken cancellationToken = default)
    {
        var movie = await context.Movies.FindAsync(movieId, cancellationToken);
        if (movie == null) throw new NotFoundException("Movie", "Id", movieId.ToString());

        context.Movies.Remove(movie);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await context.Users.FindAsync(userId, cancellationToken);
        if (user == null) throw new NotFoundException("User", "Id", userId.ToString());

        context.Users.Remove(user);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteGroupAsync(Guid groupId, CancellationToken cancellationToken = default)
    {
        var group = await context.Groups.FindAsync(groupId, cancellationToken);
        if (group == null) throw new NotFoundException("Group", "Id", groupId.ToString());

        context.Groups.Remove(group);
        await context.SaveChangesAsync(cancellationToken);
    }
}