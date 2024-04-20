using AicaDocsApi.Dto.Documents.Filter;
using AicaDocsApi.Dto.FilterCommons;
using AicaDocsUI.Dto.Documents;
using AicaDocsUI.Models;

namespace AicaDocsUI.Repositories.Documents;

public interface IDocumentRepository
{
    Task<DocumentDto?> GetDocumentById(int id);
    Task<FilterResponse<DocumentDto>?> FilterDocuments(FilterDocumentDto filter);
    Task<bool> CreateDocument(DocumentCreatedDto documentCreatedDto);
}