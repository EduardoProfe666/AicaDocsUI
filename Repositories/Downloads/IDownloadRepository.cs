using AicaDocsApi.Dto.FilterCommons;
using AicaDocsUI.Dto.Downloads;
using AicaDocsUI.Dto.Downloads.Filter;
using AicaDocsUI.Models;
using AicaDocsUI.Responses;

namespace AicaDocsUI.Repositories.Downloads;

public interface IDownloadRepository
{
    Task<DownloadDto?> GetDownloadById(int id);
    Task<FilterResponse<DownloadDto>?> GetDownloadsFilter(FilterDownloadDto filter);
    Task<string?> DownloadDocument(DownloadCreatedDto downloadCreatedDto);
}