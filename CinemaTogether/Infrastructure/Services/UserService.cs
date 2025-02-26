using Application.Common.Dto;
using Application.Common.Services;
using Application.Data;
using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Shared.Cryptography;

namespace Infrastructure.Services;

public class UserService(IApplicationDbContext context) : IUserService
{
    public async Task RegisterAsync(
        string email,
        string password,
        string username,
        CancellationToken cancellationToken = default)
    {
        var isUniqueUsername = await context.Users
            .Where(u => u.Username == username)
            .ToListAsync(cancellationToken);

        if (isUniqueUsername.Count > 0)
        {
            throw new PropertyNotUniqueException("Username");
        }
        
        var isUniqueEmail = await context.Users
            .Where(u => u.Email == email)
            .ToListAsync(cancellationToken);

        if (isUniqueEmail.Count > 0)
        {
            throw new PropertyNotUniqueException("Email");
        }
        
        await context.Users.AddAsync(
            new User
            {
                Email = email,
                Username = username,
                PasswordHash = Hasher.Hash(password),
                RoleId = CommonConstants.UserRoleId
            },
            cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<UserPublicInfoDto> GetUserPublicInfoByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await context.Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .Include(u => u.City)
            .Select(u => new UserPublicInfoDto(
                u.Username,
                u.Name,
                u.DateOfBirth,
                u.Gender.ToString(),
                u.ProfilePicturePath,
                u.City.Name,
                u.City.Country.Name))
            .FirstOrDefaultAsync(cancellationToken);
        
        if (user == null)
            throw new NotFoundException("User", "Id", userId.ToString());
        
        return user;
    }
}