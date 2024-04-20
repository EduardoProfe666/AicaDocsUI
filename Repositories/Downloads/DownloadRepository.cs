using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AicaDocsUI.Repositories.ApiData.Dto.Downloads;
using AicaDocsUI.Repositories.ApiData.Dto.Downloads.Filter;
using AicaDocsUI.Repositories.ApiData.Dto.FilterCommons;
using AicaDocsUI.Repositories.ApiData.Responses;
using AicaDocsUI.Repositories.Auth;
using AicaDocsUI.Utils.RootProviderServices;
using AicaDocsUI.Utils.TokenServices;

namespace AicaDocsUI.Repositories.Downloads;

public class DownloadRepository: IDownloadRepository
{
    private readonly HttpClient _httpClient;
    private readonly RootProvider _rootProvider;
    private readonly ITokenManager _tm;
    private readonly IAuthRepository _auth;

    public DownloadRepository(HttpClient httpClient, ITokenManager tm, IAuthRepository auth, RootProvider rootProvider)
    {
        _httpClient = httpClient;
        _tm = tm;
        _auth = auth;
        _rootProvider = rootProvider;
    }
    public async Task<DownloadDto?> GetDownloadById(int id)
    {
        if (!await _auth.IsLoginAdvance()) return null;
        var tk = _tm.GetAccessToken();
            
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tk);
        
        var response = await _httpClient.GetAsync($"{_rootProvider.RootApi}/download/{id}/");
        if (response.IsSuccessStatusCode)
        {
            var pages = await response.Content.ReadFromJsonAsync<ApiResponse<DownloadDto>>();
            return pages!.Data;
        }

        return null;
    }

    public async Task<FilterResponse<DownloadDto>?> GetDownloadsFilter(FilterDownloadDto filter)
    {
        if (!await _auth.IsLoginAdvance()) return null;
        var tk = _tm.GetAccessToken();
            
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tk);
        
        var content = new StringContent(JsonSerializer.Serialize(filter), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_rootProvider.RootApi}/download/filter/", content);
        if (response.IsSuccessStatusCode)
        {
            var pages = await response.Content.ReadFromJsonAsync<ApiResponse<FilterResponse<DownloadDto>>>();
            return pages!.Data;
        }

        Console.WriteLine(await response.Content.ReadAsStringAsync());
        return null;
    }

    public async Task<string?> DownloadDocument(DownloadCreatedDto downloadCreatedDto)
    {
        if (!await _auth.IsLoginAdvance()) return null;
        var tk = _tm.GetAccessToken();
            
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tk);
        
        var content = new StringContent(JsonSerializer.Serialize(downloadCreatedDto), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_rootProvider.RootApi}/download/", content);
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();
            return data!.Data;
        }

        Console.WriteLine(await response.Content.ReadAsStringAsync());
        return null;
    }
}