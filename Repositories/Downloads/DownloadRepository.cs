using System.Text;
using System.Text.Json;
using AicaDocsApi.Dto.FilterCommons;
using AicaDocsUI.Dto.Downloads.Filter;
using AicaDocsUI.Models;
using AicaDocsUI.Responses;
using AicaDocsUI.Utils;

namespace AicaDocsUI.Repositories.Downloads;

public class DownloadRepository: IDownloadRepository
{
    private readonly HttpClient _httpClient;
    private readonly RootProvider _rootProvider;

    public DownloadRepository(HttpClient httpClient, RootProvider rootProvider)
    {
        _httpClient = httpClient;
        _rootProvider = rootProvider;
    }
    public async Task<Download?> GetDownloadById(int id)
    {
        var response = await _httpClient.GetAsync($"{_rootProvider.RootPage}/download/{id}/");
        if (response.IsSuccessStatusCode)
        {
            var pages = await response.Content.ReadFromJsonAsync<ApiResponse<Download>>();
            return pages!.Data;
        }

        return null;
    }

    public async Task<FilterResponse<Download>?> GetDownloadsFilter(FilterDownloadDto filter)
    {
        var content = new StringContent(JsonSerializer.Serialize(filter), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_rootProvider.RootPage}/download/filter/", content);
        if (response.IsSuccessStatusCode)
        {
            var pages = await response.Content.ReadFromJsonAsync<ApiResponse<FilterResponse<Download>>>();
            return pages!.Data;
        }

        Console.WriteLine(await response.Content.ReadAsStringAsync());
        return null;
    }
}