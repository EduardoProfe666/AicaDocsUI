using System.Text;
using System.Text.Json;
using AicaDocsApi.Dto.Nomenclators;
using AicaDocsUI.Dto.Nomenclators;
using AicaDocsUI.Models;
using AicaDocsUI.Responses;
using AicaDocsUI.Utils;

namespace AicaDocsUI.Repositories.Nomenclators;

public class NomenclatorRepository : INomenclatorRepository
{
    private readonly HttpClient _httpClient;
    private readonly RootProvider _rootProvider;

    public NomenclatorRepository(HttpClient httpClient, RootProvider rootProvider)
    {
        _httpClient = httpClient;
        _rootProvider = rootProvider;
    }

    public async Task<IEnumerable<Nomenclator>?> GetNomenclatorsByTypeAsync(int type)
    {
        var response = await _httpClient.GetAsync($"{_rootProvider.RootPage}/nomenclator/{type}");
        if (response.IsSuccessStatusCode)
        {
            var nomencladores = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<Nomenclator>>>();
            return nomencladores!.Data!;
        }
        return null;
    }

    public async Task<Nomenclator> GetNomenclatorAsync(int type, int id)
    {
        var response = await _httpClient.GetAsync($"{_rootProvider.RootPage}/nomenclator/{type}/{id}");
        if (response.IsSuccessStatusCode)
        {
            var nomenclador = await response.Content.ReadFromJsonAsync<ApiResponse<Nomenclator>>();
            return nomenclador!.Data!;
        }
        return null;
    }

    public async Task CreateNomenclatorAsync(NomenclatorCreatedDto nomenclator)
    {
        var content = new StringContent(JsonSerializer.Serialize(nomenclator), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_rootProvider.RootPage}/nomenclator/", content);
    }

    public async Task PatchNomenclatorAsync(int id, NomenclatorPatchDto name)
    {
        var content = new StringContent(JsonSerializer.Serialize(name), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_rootProvider.RootPage}/nomenclator/{id}/", content);
    }
}