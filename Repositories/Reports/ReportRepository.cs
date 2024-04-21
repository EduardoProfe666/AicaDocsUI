using System.Net.Http.Headers;
using AicaDocsUI.Repositories.Auth;
using AicaDocsUI.Utils.RootProviderServices;
using AicaDocsUI.Utils.TokenServices;

namespace AicaDocsUI.Repositories.Reports;

public class ReportRepository: IReportRepository
{
    private readonly HttpClient _httpClient;
    private readonly RootProvider _rootProvider;
    private readonly ITokenManager _tm;
    private readonly IAuthRepository _auth;

    public ReportRepository(HttpClient httpClient, RootProvider rootProvider, ITokenManager tm, IAuthRepository auth)
    {
        _httpClient = httpClient;
        _rootProvider = rootProvider;
        _tm = tm;
        _auth = auth;
    }


    public async Task<Stream?> GetReportUsersAsync()
    {
        if (!await _auth.IsLoginAdvanceAsync()) return null;
        var tk = _tm.GetAccessToken();
            
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tk);
        
        var response = await _httpClient.GetAsync($"{_rootProvider.RootApi}/report/users/");
        if (response.IsSuccessStatusCode)
        {
            var memoryStream = new MemoryStream();
            await response.Content.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }

        return null;
    }

    public async Task<Stream?> GetReportUserByRoleAsync(int role)
    {
        if (!await _auth.IsLoginAdvanceAsync()) return null;
        var tk = _tm.GetAccessToken();
            
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tk);
        
        var response = await _httpClient.GetAsync($"{_rootProvider.RootApi}/report/users/{role}");
        if (response.IsSuccessStatusCode)
        {
            var memoryStream = new MemoryStream();
            await response.Content.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }

        return null;
    }

    public async Task<Stream?> GetDocumentsAsync()
    {
        if (!await _auth.IsLoginAdvanceAsync()) return null;
        var tk = _tm.GetAccessToken();
            
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tk);
        
        var response = await _httpClient.GetAsync($"{_rootProvider.RootApi}/report/documents/");
        if (response.IsSuccessStatusCode)
        {
            var memoryStream = new MemoryStream();
            await response.Content.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }

        return null;
    }

    public async Task<Stream?> GetDocumentsByUserAsync(string user)
    {
        if (!await _auth.IsLoginAdvanceAsync()) return null;
        var tk = _tm.GetAccessToken();
            
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tk);
        
        var response = await _httpClient.GetAsync($"{_rootProvider.RootApi}/report/documents/{user}");
        if (response.IsSuccessStatusCode)
        {
            var memoryStream = new MemoryStream();
            await response.Content.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }

        return null;
    }

    public async Task<Stream?> GetDownloadsAsync()
    {
        if (!await _auth.IsLoginAdvanceAsync()) return null;
        var tk = _tm.GetAccessToken();
            
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tk);
        
        var response = await _httpClient.GetAsync($"{_rootProvider.RootApi}/report/downloads/");
        if (response.IsSuccessStatusCode)
        {
            var memoryStream = new MemoryStream();
            await response.Content.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }

        return null;
    }

    public async Task<Stream?> GetDownloadByUserAsync(string user)
    {
        if (!await _auth.IsLoginAdvanceAsync()) return null;
        var tk = _tm.GetAccessToken();
            
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tk);
        
        var response = await _httpClient.GetAsync($"{_rootProvider.RootApi}/report/downloads/{user}");
        if (response.IsSuccessStatusCode)
        {
            var memoryStream = new MemoryStream();
            await response.Content.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }

        return null;
    }

    public async Task<Stream?> GetNomenclatorByTypeAsync(int type)
    {
        if (!await _auth.IsLoginAdvanceAsync()) return null;
        var tk = _tm.GetAccessToken();
            
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tk);
        
        var response = await _httpClient.GetAsync($"{_rootProvider.RootApi}/report/nomenclators/{type}");
        if (response.IsSuccessStatusCode)
        {
            var memoryStream = new MemoryStream();
            await response.Content.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }

        return null;
    }
}