using AicaDocsUI.Models;
using AicaDocsUI.Responses;
using AicaDocsUI.Utils;

namespace AicaDocsUI.Repositories.Utils;

public class PaginationRepository : IPaginationRepository
{
    private readonly HttpClient _httpClient;
    private readonly RootProvider _rootProvider;

    public PaginationRepository(HttpClient httpClient, RootProvider rootProvider)
    {
        _httpClient = httpClient;
        _rootProvider = rootProvider;
    }
    public async Task<int?> GetCountPages(int type, int pageSize)
    {
        var response = await _httpClient.GetAsync($"{_rootProvider.RootPage}/pagination/pages/{type}/{pageSize}");
        if (response.IsSuccessStatusCode)
        {
            var pages = await response.Content.ReadFromJsonAsync<ApiResponse<int>>();
            return pages!.Data!;
        }

        return null;
    }
}