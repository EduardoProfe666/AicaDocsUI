using System.ComponentModel.DataAnnotations;

namespace AicaDocsUI.Repositories.ApiData.Dto.Auth;

public class ChangePasswordDto
{
    public required string OldPassword { get; set; }
    
    public required string NewPassword { get; set; }
}