using AicaDocsUI.Models.IdentityCommons;

namespace AicaDocsUI.Models.Users;

public class UserDataDto
{
    public required string FullName { get; set; }
    
    public required string Email { get; set; }

    public required bool IsConfirmed { get; set; }

    public required UserRole Role { get; set; }
}