using AicaDocsUI.Repositories.ApiData.Dto.Auth;
using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;
using AicaDocsUI.Repositories.ApiData.Dto.Users;
using Microsoft.AspNetCore.Identity.Data;

namespace AicaDocsUI.Repositories.Auth;

public interface IAuthRepository
{
    Task<bool> Login(LoginRequestDto login);
    void Logout();
    Task<bool> Register(string email, string fullName, UserRole role);
    Task<bool> ValidateToken();
    Task<bool> RefreshToken();
    Task<bool> ConfirmEmail(ConfirmEmailDto confirmEmail);
    Task<bool> ForgotPassword(string email);
    Task<bool> ResetPassword(ResetPasswordRequest resetRequest);
    Task<UserDataDto?> GetInfo();
    Task<bool> ChangePassword(ChangePasswordDto chd);

    Task<UserRole?> GetUserRole();

    bool IsLogin();

    Task<bool> LoginAdvance(LoginRequestDto login);

    Task<bool> IsLoginAdvance();

}