using Application.Common.Dto;

namespace Application.Common.Auth;

public interface ITokenProvider
{
    string Create(UserDto account);
}