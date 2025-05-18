using Application.Common.Dto;
using Application.Common.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api")]
public class AdminController(IAdminService adminService) : ControllerBase
{
    [HttpPut("movies/{id:guid}")]
    public async Task<IResult> UpdateMovie(Guid id, [FromBody] MovieUpdateDto movie, CancellationToken cancellationToken = default)
    {
        if (id != movie.Id)
            return Results.BadRequest();

        await adminService.UpdateMovieAsync(movie, cancellationToken);
        return Results.NoContent();
    }

    [HttpDelete("movies/{id:guid}")]
    public async Task<IResult> DeleteMovie(Guid id, CancellationToken cancellationToken = default)
    {
        await adminService.DeleteMovieAsync(id, cancellationToken);

        return Results.NoContent();
    }

    [HttpDelete("users/{id:guid}")]
    public async Task<IResult> DeleteUser(Guid id, CancellationToken cancellationToken = default)
    {
        await adminService.DeleteUserAsync(id, cancellationToken);

        return Results.NoContent();
    }

    [HttpDelete("groups/{id:guid}")]
    public async Task<IResult> DeleteGroup(Guid id, CancellationToken cancellationToken = default)
    {
        await adminService.DeleteGroupAsync(id, cancellationToken);

        return Results.NoContent();
    }
}