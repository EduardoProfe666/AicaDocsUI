using AicaDocsApi.Dto.Documents.Filter;
using AicaDocsApi.Dto.FilterCommons;
using AicaDocsUI.Dto.Documents;
using AicaDocsUI.Models;

namespace AicaDocsUI.Repositories.Documents;

public interface IDocumentRepository
{
    Task<Document?> GetDocumentById(int id);
    Task<FilterResponse<Document>?> FilterDocuments(FilterDocumentDto filter);
    Task<bool> CreateDocument(DocumentCreatedDto documentCreatedDto);
}