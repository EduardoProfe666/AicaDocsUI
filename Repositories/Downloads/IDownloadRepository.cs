using AicaDocsUI.Repositories.ApiData.Dto.Downloads;
using AicaDocsUI.Repositories.ApiData.Dto.Downloads.Filter;
using AicaDocsUI.Repositories.ApiData.Dto.FilterCommons;

namespace AicaDocsUI.Repositories.Downloads;

public interface IDownloadRepository
{
    Task<DownloadDto?> GetDownloadByIdAsync(int id);
    Task<FilterResponse<DownloadDto>?> GetDownloadsFilterAsync(FilterDownloadDto filter);
    Task<string?> DownloadDocumentAsync(DownloadCreatedDto downloadCreatedDto);
}