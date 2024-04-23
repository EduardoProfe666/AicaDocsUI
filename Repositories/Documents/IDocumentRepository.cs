using AicaDocsUI.Repositories.ApiData.Dto.Documents;
using AicaDocsUI.Repositories.ApiData.Dto.Documents.Filter;
using AicaDocsUI.Repositories.ApiData.Dto.FilterCommons;

namespace AicaDocsUI.Repositories.Documents;

public interface IDocumentRepository
{
    Task<DocumentDto?> GetDocumentByIdAsync(int id);
    Task<FilterResponse<DocumentDto>?> FilterDocumentsAsync(FilterDocumentDto filter);
    Task<bool> CreateDocumentAsync(DocumentCreatedDto documentCreatedDto);
}