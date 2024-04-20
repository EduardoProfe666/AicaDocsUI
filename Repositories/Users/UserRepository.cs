using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AicaDocsApi.Dto.FilterCommons;
using AicaDocsUI.Models.Users;
using AicaDocsUI.Models.Users.Filter;
using AicaDocsUI.Repositories.Auth;
using AicaDocsUI.Responses;
using AicaDocsUI.Utils.RootProviderServices;
using AicaDocsUI.Utils.TokenServices;

namespace AicaDocsUI.Repositories.Users;

public class UserRepository: IUserRepository
{
    private readonly HttpClient _httpClient;
    private readonly RootProvider _rootProvider;
    private readonly ITokenManager _tm;
    private readonly IAuthRepository _auth;
    
    public UserRepository(HttpClient httpClient, RootProvider rootProvider, ITokenManager tm, IAuthRepository auth)
    {
        _httpClient = httpClient;
        _rootProvider = rootProvider;
        _tm = tm;
        _auth = auth;
    }

    public async Task<UserDataDto?> GetUserData(string email)
    {
        if (!await _auth.IsLoginAdvance()) return null;
        var tk = _tm.GetAccessToken();
            
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tk);
        
        var response = await _httpClient.GetAsync($"{_rootProvider.RootApi}/user/{email}/");
        if (response.IsSuccessStatusCode)
        {
            var userData = await response.Content.ReadFromJsonAsync<ApiResponse<UserDataDto>>();
            return userData!.Data;
        }

        return null;
        
    }

    public async Task<FilterResponse<UserDataDto>?> GetUserDataFilter(FilterUserDto filter)
    {
        if (!await _auth.IsLoginAdvance()) return null;
        var tk = _tm.GetAccessToken();
            
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tk);
        
        var content = new StringContent(JsonSerializer.Serialize(filter), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_rootProvider.RootApi}/user/filter/", content);
        if (response.IsSuccessStatusCode)
        {
            var users = await response.Content.ReadFromJsonAsync<ApiResponse<FilterResponse<UserDataDto>>>();
            return users!.Data;
        }

        Console.WriteLine(await response.Content.ReadAsStringAsync());
        return null;
    }

    public async Task<bool> DeleteUser(string email)
    {
        if (!await _auth.IsLoginAdvance()) return false;
        var tk = _tm.GetAccessToken();
            
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tk);
        
        var response = await _httpClient.DeleteAsync($"{_rootProvider.RootApi}/user/{email}");
        return response.IsSuccessStatusCode;
    }
}