using Application.Common.Dto;
using Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Users.Handlers;

internal class GetUserByEmailQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetUserByEmailQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .Include(a => a.Role)
            .Where(u => u.Email == query.Email)
            .AsNoTracking()
            .Select(a => new UserDto(
                    a.Id,
                    a.Email,
                    a.PasswordHash,
                    a.Role.Name)
                )
            .FirstOrDefaultAsync(cancellationToken);
        
        if (user == null)
            throw new Exception();
        
        return user;
    }
}