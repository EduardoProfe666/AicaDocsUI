using AicaDocsUI.Repositories.ApiData.Dto.Auth;
using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;
using AicaDocsUI.Repositories.ApiData.Dto.Users;
using Microsoft.AspNetCore.Identity.Data;

namespace AicaDocsUI.Repositories.Auth;

public interface IAuthRepository
{
    Task<bool> LoginAsync(LoginRequestDto login);
    void Logout();
    Task<bool> RegisterAsync(string email, string fullName, UserRole role);
    Task<bool> ValidateTokenAsync();
    Task<bool> RefreshTokenAsync();
    Task<bool> ConfirmEmailAsync(ConfirmEmailDto confirmEmail);
    Task<bool> ForgotPasswordAsync(string email);
    Task<bool> ResetPasswordAsync(ResetPasswordRequest resetRequest);
    Task<UserDataDto?> GetInfoAsync();
    Task<bool> ChangePasswordAsync(ChangePasswordDto chd);

    Task<UserRole?> GetUserRoleAsync();

    Task<bool> IsLoginAsync();

    Task<bool> LoginAdvanceAsync(LoginRequestDto login);

    Task<bool> IsLoginAdvanceAsync();

}