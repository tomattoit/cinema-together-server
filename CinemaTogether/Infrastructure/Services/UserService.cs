using Application.Common.Dto;
using Application.Common.Services;
using Application.Data;
using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Shared.Cryptography;

namespace Infrastructure.Services;

public class UserService(
    IApplicationDbContext context,
    IEmailSender emailSender,
    LinkGenerator linkGenerator,
    IHttpContextAccessor httpContextAccessor) : IUserService
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
        
        var userId = Guid.NewGuid();
        var tokenId = Guid.NewGuid();
        
        await context.Users.AddAsync(
            new User
            {
                Id = userId,
                Email = email,
                Username = username,
                PasswordHash = Hasher.Hash(password),
                Role = Role.User,
                IsEmailVerified = false
            },
            cancellationToken);

        await context.EmailVerificationTokens.AddAsync(
            new EmailVerificationToken
            {
                Id = tokenId,
                UserId = userId,
            },
            cancellationToken);
        
        var link = GenerateLink(tokenId.ToString());
        var subject = "Cinema Together email confirmation";
        var message = "Please confirm your email address by clicking this <a href=\"" + link + "\">link</a>";
        
        await emailSender.SendEmailAsync(email, subject, message, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task VerifyEmailAsync(Guid tokenId, CancellationToken cancellationToken = default)
    {
        var token = await context.EmailVerificationTokens
            .FirstOrDefaultAsync(t => t.Id == tokenId, cancellationToken);
        
        if (token == null)
            throw new NotFoundException("Email Verification Token", "Id", tokenId.ToString());
        
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Id == token.UserId, cancellationToken);
        
        if (user == null)
            throw new NotFoundException("User", "Id", token.UserId.ToString());
        
        user.IsEmailVerified = true;

        context.EmailVerificationTokens.Remove(token);
        
        await context.SaveChangesAsync(cancellationToken);
    }

    private string GenerateLink(string tokenId) => linkGenerator.GetUriByName(
        httpContextAccessor.HttpContext!,
        CommonConstants.VerifyEmailEndpointName,
        new { verificationTokenId = tokenId }) ?? string.Empty;

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
    
    public async Task<UserPrivateInfoDto> GetUserPrivateInfoByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await context.Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .Include(u => u.City)
            .Select(u => new UserPrivateInfoDto(
                u.Email,
                u.Username,
                u.TwoFactorEnabled,
                u.Role == Role.Admin,
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

    public async Task UpdateProfileInfoAsync(Guid userId, UpdateUserProfileDto userDto, CancellationToken cancellationToken = default)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        
        if (user == null)
            throw new NotFoundException("User", "Id", userId.ToString());
        
        if (user.Email == userDto.Email)
            throw new PropertyNotUniqueException("Email");
        
        if (user.Username == userDto.Username)
            throw new PropertyNotUniqueException("Username");
            
        user.Email = userDto.Email;
        user.Name = userDto.Name;
        user.CityId = userDto.CityId;
        user.Gender = userDto.Gender;
        user.ProfilePicturePath = userDto.ProfilePicturePath;
        user.DateOfBirth = userDto.DateOfBirth;
        user.Username = userDto.Username;
        
        await context.SaveChangesAsync(cancellationToken);
    }

    public List<GenderDto> GetGenders()
    {
        var genderList = Enum.GetValues(typeof(Gender))
            .Cast<Gender>()
            .Select(g => new GenderDto(g, g.ToString()))
            .ToList();
        
        return genderList;
    }

    public async Task<PaginatedResponse<UserListItemDto>> GetUsersAsync(
        int page,
        int pageSize,
        string searchString,
        CancellationToken cancellationToken)
    {
        var query = context.Users.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            query = query.Where(u => u.Name.Contains(searchString) || u.Username.Contains(searchString));
        }
        
        var totalCount = await query.CountAsync(cancellationToken);
        
        var users = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(u => new UserListItemDto(u.Id, u.Username, u.Name, u.Rating))
            .ToListAsync(cancellationToken);

        var paginatedResponse = new PaginatedResponse<UserListItemDto>(users, totalCount, page, pageSize);
        
        return paginatedResponse;
    }
}