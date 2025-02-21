namespace Application.Common.Services;

public interface ILoginService
{
    Task<string> Login(string email, string password, CancellationToken cancellationToken);
}