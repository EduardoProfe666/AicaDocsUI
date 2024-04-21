using System.ComponentModel.DataAnnotations;

namespace AicaDocsUI.Repositories.ApiData.Dto.Auth;

public class ConfirmEmailDto
{
    public required string Email { get; set; }
    
    public required string ConfirmEmailToken { get; set; }
    
    public required string ChangePasswordToken { get; set; }
    
    public required string Password { get; set; }
}