using AicaDocsUI.Repositories.ApiData.Dto.Downloads;
using AicaDocsUI.Repositories.ApiData.Dto.Downloads.Filter;
using AicaDocsUI.Repositories.ApiData.Dto.FilterCommons;

namespace AicaDocsUI.Repositories.Downloads;

public interface IDownloadRepository
{
    Task<DownloadDto?> GetDownloadById(int id);
    Task<FilterResponse<DownloadDto>?> GetDownloadsFilter(FilterDownloadDto filter);
    Task<string?> DownloadDocument(DownloadCreatedDto downloadCreatedDto);
}