namespace Domain.Constants;

public static class CommonConstants
{
    public const string AdminRole = "Admin";
    
    public const string UserRole = "User";
    
    public static readonly Guid UserRoleId = Guid.Parse("22222222-2222-2222-2222-222222222222");
    
    public static readonly Guid AdmiRoleId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    
    public const string DefaultTokenName = "token";
    
    public const string VerifyEmailEndpointName = "verifyEmail";
}