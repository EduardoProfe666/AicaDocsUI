using System.ComponentModel.DataAnnotations;

namespace AicaDocsUI.Models.Auth;

public class ForgotPasswordDto
{
    public required string Email { get; set; }
    public required string UrlView { get; init; }
}