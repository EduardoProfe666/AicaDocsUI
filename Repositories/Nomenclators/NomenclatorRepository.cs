using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AicaDocsUI.Repositories.ApiData.Dto.Nomenclators;
using AicaDocsUI.Repositories.ApiData.Responses;
using AicaDocsUI.Repositories.Auth;
using AicaDocsUI.Utils.RootProviderServices;
using AicaDocsUI.Utils.TokenServices;

namespace AicaDocsUI.Repositories.Nomenclators;

public class NomenclatorRepository : INomenclatorRepository
{
    private readonly HttpClient _httpClient;
    private readonly RootProvider _rootProvider;
    private readonly ITokenManager _tm;
    private readonly IAuthRepository _auth;

    public NomenclatorRepository(HttpClient httpClient, RootProvider rootProvider, ITokenManager tm,
        IAuthRepository auth)
    {
        _httpClient = httpClient;
        _rootProvider = rootProvider;
        _tm = tm;
        _auth = auth;
    }

    public async Task<IEnumerable<NomenclatorDto>?> GetNomenclatorsByTypeAsync(int type)
    {
        if (!await _auth.IsLoginAdvanceAsync()) return null;
        var tk = _tm.GetAccessToken();

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tk);

        var response = await _httpClient.GetAsync($"{_rootProvider.RootApi}/nomenclator/{type}");
        if (response.IsSuccessStatusCode)
        {
            var nomencladores = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<NomenclatorDto>>>();
            return nomencladores!.Data!.OrderBy(x => x.Name);
        }

        return null;
    }

    public async Task<NomenclatorDto?> GetNomenclatorAsync(int type, int id)
    {
        if (!await _auth.IsLoginAdvanceAsync()) return null;
        var tk = _tm.GetAccessToken();

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tk);

        var response = await _httpClient.GetAsync($"{_rootProvider.RootApi}/nomenclator/{type}/{id}");
        if (response.IsSuccessStatusCode)
        {
            var nomenclador = await response.Content.ReadFromJsonAsync<ApiResponse<NomenclatorDto>>();
            return nomenclador!.Data!;
        }

        return null;
    }

    public async Task<bool> CreateNomenclatorAsync(NomenclatorCreatedDto nomenclator)
    {
        if (!await _auth.IsLoginAdvanceAsync()) return false;
        var tk = _tm.GetAccessToken();

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tk);

        var content = new StringContent(JsonSerializer.Serialize(nomenclator), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_rootProvider.RootApi}/nomenclator/", content);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> PutNomenclatorAsync(int id, NomenclatorPutDto name)
    {
        if (!await _auth.IsLoginAdvanceAsync()) return false;
        var tk = _tm.GetAccessToken();

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tk);

        var content = new StringContent(JsonSerializer.Serialize(name), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"{_rootProvider.RootApi}/nomenclator/{id}/", content);
        
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteNomenclatorAsync(int type, int id)
    {
        if (!await _auth.IsLoginAdvanceAsync()) return false;
        var tk = _tm.GetAccessToken();

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tk);
        
        var response = await _httpClient.DeleteAsync($"{_rootProvider.RootApi}/nomenclator/{type}/{id}");
        return response.IsSuccessStatusCode;
    }
}