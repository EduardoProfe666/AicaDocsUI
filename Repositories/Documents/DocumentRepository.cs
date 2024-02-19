using System.Text;
using System.Text.Json;
using AicaDocsApi.Dto.Documents.Filter;
using AicaDocsApi.Dto.FilterCommons;
using AicaDocsUI.Dto.Documents;
using AicaDocsUI.Models;
using AicaDocsUI.Responses;
using AicaDocsUI.Utils;

namespace AicaDocsUI.Repositories.Documents;

public class DocumentRepository : IDocumentRepository
{
    private readonly HttpClient _httpClient;
    private readonly RootProvider _rootProvider;

    public DocumentRepository(HttpClient httpClient, RootProvider rootProvider)
    {
        _httpClient = httpClient;
        _rootProvider = rootProvider;
    }
    public async Task<Document?> GetDocumentById(int id)
    {
        var response = await _httpClient.GetAsync($"{_rootProvider.RootPage}/document/{id}/");
        if (response.IsSuccessStatusCode)
        {
            var pages = await response.Content.ReadFromJsonAsync<ApiResponse<Document>>();
            return pages!.Data;
        }

        return null;
    }

    public async Task<FilterResponse<Document>?> FilterDocuments(FilterDocumentDto filter)
    {
        var content = new StringContent(JsonSerializer.Serialize(filter), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_rootProvider.RootPage}/document/filter/", content);
        if (response.IsSuccessStatusCode)
        {
            var pages = await response.Content.ReadFromJsonAsync<ApiResponse<FilterResponse<Document>>>();
            return pages!.Data;
        }
        return null;
    }

    public async Task<bool> CreateDocument(DocumentCreatedDto documentCreatedDto)
    {
        var content = new StringContent(JsonSerializer.Serialize(documentCreatedDto), Encoding.UTF8, "multipart/form-data");
        var response = await _httpClient.PostAsync($"{_rootProvider.RootPage}/document/", content);
        return response.IsSuccessStatusCode;
    }
}