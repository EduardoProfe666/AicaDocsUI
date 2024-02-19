using AicaDocsApi.Dto.FilterCommons;
using AicaDocsUI.Dto.Downloads.Filter;
using AicaDocsUI.Models;
using AicaDocsUI.Responses;

namespace AicaDocsUI.Repositories.Downloads;

public interface IDownloadRepository
{
    Task<Download?> GetDownloadById(int id);
    Task<FilterResponse<Download>?> GetDownloadsFilter(FilterDownloadDto filter);
}