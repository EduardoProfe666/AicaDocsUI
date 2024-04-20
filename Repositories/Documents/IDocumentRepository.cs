using AicaDocsUI.Models;
using AicaDocsUI.Repositories.ApiData.Dto.Documents;
using AicaDocsUI.Repositories.ApiData.Dto.Documents.Filter;
using AicaDocsUI.Repositories.ApiData.Dto.FilterCommons;

namespace AicaDocsUI.Repositories.Documents;

public interface IDocumentRepository
{
    Task<DocumentDto?> GetDocumentById(int id);
    Task<FilterResponse<DocumentDto>?> FilterDocuments(FilterDocumentDto filter);
    Task<bool> CreateDocument(DocumentCreatedDto documentCreatedDto);
}