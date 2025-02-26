namespace Application.Common.Services;

public interface IRegisterService
{
    Task RegisterAsync(string email, string password, string username, CancellationToken cancellationToken);
}