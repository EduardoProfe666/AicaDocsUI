using System.ComponentModel.DataAnnotations;

namespace AicaDocsUI.Repositories.ApiData.Dto.Auth;

public class ForgotPasswordDto
{
    public required string Email { get; set; }
    public required string UrlView { get; init; }
}