using System.Net.Http.Headers;
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
        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(documentCreatedDto.Title), "title");
        content.Add(new StringContent(documentCreatedDto.Code), "code");
        content.Add(new StringContent(documentCreatedDto.Edition.ToString()), "edition");
        content.Add(new StringContent(documentCreatedDto.DateOfValidity.ToString("O")), "dateOfValidity");
        content.Add(new StringContent(documentCreatedDto.TypeId.ToString()), "typeId");
        content.Add(new StringContent(documentCreatedDto.ProcessId.ToString()), "processId");
        content.Add(new StringContent(documentCreatedDto.ScopeId.ToString()), "scopeId");
        
        using var pdfStreamContent = new StreamContent(documentCreatedDto.Pdf.OpenReadStream());
        pdfStreamContent.Headers.ContentType = MediaTypeHeaderValue.Parse(documentCreatedDto.Pdf.ContentType);
        
        using var wordStreamContent = new StreamContent(documentCreatedDto.Word.OpenReadStream());
        wordStreamContent.Headers.ContentType = MediaTypeHeaderValue.Parse(documentCreatedDto.Word.ContentType);
        
        content.Add(pdfStreamContent, "pdf", documentCreatedDto.Pdf.FileName);
        content.Add(wordStreamContent, "word", documentCreatedDto.Word.FileName);
        
        var response = await _httpClient.PostAsync($"{_rootProvider.RootPage}/document/", content);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        return response.IsSuccessStatusCode;
    }
}