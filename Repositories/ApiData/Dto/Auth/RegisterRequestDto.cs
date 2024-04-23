using System.ComponentModel.DataAnnotations;
using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;
using Microsoft.Extensions.Options;

namespace AicaDocsUI.Repositories.ApiData.Dto.Auth;

public class RegisterRequestDto
{
    public required string Email { get; set; }
    
    public required string FullName { get; set; }

    public required UserRole Role { get; set; }
    
    public required string UrlView { get; set; }
}