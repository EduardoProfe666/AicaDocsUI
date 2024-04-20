using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AicaDocsUI.Models;
using AicaDocsUI.Repositories.ApiData.Dto.Documents;
using AicaDocsUI.Repositories.ApiData.Dto.Documents.Filter;
using AicaDocsUI.Repositories.ApiData.Dto.FilterCommons;
using AicaDocsUI.Repositories.ApiData.Responses;
using AicaDocsUI.Repositories.Auth;
using AicaDocsUI.Utils.RootProviderServices;
using AicaDocsUI.Utils.TokenServices;

namespace AicaDocsUI.Repositories.Documents;

public class DocumentRepository : IDocumentRepository
{
    private readonly HttpClient _httpClient;
    private readonly RootProvider _rootProvider;
    private readonly ITokenManager _tm;
    private readonly IAuthRepository _auth;

    public DocumentRepository(HttpClient httpClient, ITokenManager tm, IAuthRepository auth, RootProvider rootProvider)
    {
        _httpClient = httpClient;
        _rootProvider = rootProvider;
        _tm = tm;
        _auth = auth;
    }
    public async Task<DocumentDto?> GetDocumentById(int id)
    {
        if (!await _auth.IsLoginAdvance()) return null;
        var tk = _tm.GetAccessToken();
            
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tk);
        var response = await _httpClient.GetAsync($"{_rootProvider.RootApi}/document/{id}/");
        if (response.IsSuccessStatusCode)
        {
            var pages = await response.Content.ReadFromJsonAsync<ApiResponse<DocumentDto>>();
            return pages!.Data;
        }

        return null;

    }

    public async Task<FilterResponse<DocumentDto>?> FilterDocuments(FilterDocumentDto filter)
    {
        if (!await _auth.IsLoginAdvance()) return null;
        var tk = _tm.GetAccessToken();
            
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tk);
        var content = new StringContent(JsonSerializer.Serialize(filter), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_rootProvider.RootApi}/document/filter/", content);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        if (response.IsSuccessStatusCode)
        {
            var pages = await response.Content.ReadFromJsonAsync<ApiResponse<FilterResponse<DocumentDto>>>();
            return pages!.Data;
        }
        return null;
    }

    public async Task<bool> CreateDocument(DocumentCreatedDto documentCreatedDto)
    {
        if (!await _auth.IsLoginAdvance()) return false;
        var tk = _tm.GetAccessToken();
            
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tk);
        
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
        
        var response = await _httpClient.PostAsync($"{_rootProvider.RootApi}/document/", content);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        return response.IsSuccessStatusCode;
    }
}