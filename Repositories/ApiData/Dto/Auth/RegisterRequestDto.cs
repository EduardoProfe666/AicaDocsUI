using AicaDocsUI.Models.IdentityCommons;

namespace AicaDocsUI.Models.Auth;

public class RegisterRequestDto
{
    public required string Email { get; init; }
    public required string FullName { get; init; }
    public required UserRole Role { get; init; }
    
    public required string UrlView { get; init; }
}