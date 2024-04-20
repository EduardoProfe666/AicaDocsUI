namespace AicaDocsUI.Models.Auth;

public class LoginRequestDto
{

    public required string Email { get; init; }
    
    public required string Password { get; init; }
}