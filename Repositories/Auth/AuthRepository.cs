using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AicaDocsUI.Repositories.ApiData.Dto.Auth;
using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;
using AicaDocsUI.Repositories.ApiData.Dto.Users;
using AicaDocsUI.Repositories.ApiData.Responses;
using AicaDocsUI.Utils.RootProviderServices;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity.Data;
using AicaDocsUI.Utils.TokenServices;

namespace AicaDocsUI.Repositories.Auth;

public class AuthRepository: IAuthRepository
{
    private readonly HttpClient _httpClient;
    private readonly RootProvider _rootProvider;
    private readonly ITokenManager _tm;
    
    public AuthRepository(HttpClient httpClient, RootProvider rootProvider, ITokenManager tm)
    {
        _httpClient = httpClient;
        _rootProvider = rootProvider;
        _tm = tm;
    }
    
    public async Task<bool> Login(LoginRequestDto login)
    {
        var content = new StringContent(JsonSerializer.Serialize(login), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_rootProvider.RootApi}/auth/login", content);
        if (response.IsSuccessStatusCode)
        {
            var tok = await response.Content.ReadFromJsonAsync<AccessTokenResponse>();
            
            _tm.SaveTokens(tok!.AccessToken, tok!.RefreshToken);
            
            return true;
        }

        return false;

    }

    public void Logout()
    {
        _tm.DeleteTokens();
    }

    public async Task<bool> Register(string email, string fullName, UserRole role)
    {
        if (!await IsLoginAdvance()) return false;
        var tk = _tm.GetAccessToken();
            
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tk);
        
        RegisterRequestDto registration = new RegisterRequestDto
        {
            Email = email,
            FullName = fullName,
            Role = role,
            UrlView = $"{_rootProvider.RootUI}/Account/ConfirmEmail"
        };
        var content = new StringContent(JsonSerializer.Serialize(registration), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_rootProvider.RootApi}/auth/register", content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ValidateToken()
    {
        var accessToken = _tm.GetAccessToken();
        if (accessToken is null)
            return false;
        
        var request = new HttpRequestMessage();
        request.RequestUri = new Uri($"{_rootProvider.RootApi}/auth/validateToken");
        request.Method = HttpMethod.Get;

        request.Headers.Add("Authorization", $"Bearer {accessToken}");
        
        var response = await _httpClient.SendAsync(request);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RefreshToken()
    {
        var refreshToken = _tm.GetRefreshToken();
        if (refreshToken is null)
            return false;

        var rr = new RefreshRequest
        {
            RefreshToken = refreshToken
        };
        
        var content = new StringContent(JsonSerializer.Serialize(rr), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_rootProvider.RootApi}/auth/refresh", content);
        if (response.IsSuccessStatusCode)
        {
            var tok = await response.Content.ReadFromJsonAsync<AccessTokenResponse>();
            
            _tm.SaveTokens(tok!.AccessToken, tok!.RefreshToken);
            
            return true;
        }

        return false;
    }

    public async Task<bool> ConfirmEmail(ConfirmEmailDto confirmEmail)
    {
        var content = new StringContent(JsonSerializer.Serialize(confirmEmail), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_rootProvider.RootApi}/auth/confirmEmail", content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ForgotPassword(string email)
    {
        var fp = new ForgotPasswordDto(){Email = email, UrlView = $"{_rootProvider.RootUI}/Account/ResetPassword"};
        var content = new StringContent(JsonSerializer.Serialize(fp), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_rootProvider.RootApi}/auth/forgotPassword", content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ResetPassword(ResetPasswordRequest resetRequest)
    {
        var content = new StringContent(JsonSerializer.Serialize(resetRequest), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_rootProvider.RootApi}/auth/forgotPassword", content);
        return response.IsSuccessStatusCode;
    }

    public async Task<UserDataDto?> GetInfo()
    {
        var accessToken = _tm.GetAccessToken();
        if (accessToken is null)
            return null;
        
        var request = new HttpRequestMessage();
        request.RequestUri = new Uri($"{_rootProvider.RootApi}/auth/manage/info");
        request.Method = HttpMethod.Get;

        request.Headers.Add("Authorization", $"Bearer {accessToken}");
        
        var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var ud = await response.Content.ReadFromJsonAsync<ApiResponse<UserDataDto>>();
            
            return ud!.Data;
        }

        return null;
    }

    public async Task<bool> ChangePassword(ChangePasswordDto chd)
    {
        var accessToken = _tm.GetAccessToken();
        if (accessToken is null)
            return false;
        
        var request = new HttpRequestMessage();
        request.RequestUri = new Uri($"{_rootProvider.RootApi}/auth/manage/changePassword");
        request.Method = HttpMethod.Post;

        request.Headers.Add("Authorization", $"Bearer {accessToken}");
        
        var content = new StringContent(JsonSerializer.Serialize(chd), Encoding.UTF8, "application/json");
        request.Content = content;
        
        var response = await _httpClient.SendAsync(request);
        return response.IsSuccessStatusCode;
    }

    public async Task<UserRole?> GetUserRole()
    {
        var info = await GetInfo();
        return info?.Role;
    }

    public bool IsLogin()
    {
        var refreshToken = _tm.GetRefreshToken();
        return refreshToken is not null;
    }

    public async Task<bool> LoginAdvance(LoginRequestDto login)
    {
        var b = await Login(login);
        if (!b)
            b = await RefreshToken();
        if(!b)
            _tm.DeleteTokens();
        return b;
    }

    public async Task<bool> IsLoginAdvance()
    {
        var b = IsLogin();
        if (!b)
            b = await RefreshToken();
        return b;
    }
}