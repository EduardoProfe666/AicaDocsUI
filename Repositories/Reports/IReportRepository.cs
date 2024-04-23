namespace AicaDocsUI.Repositories.Reports;

public interface IReportRepository
{
    Task<Stream?> GetReportUsersAsync();
    Task<Stream?> GetReportUserByRoleAsync(int role);
    Task<Stream?> GetDocumentsAsync();
    Task<Stream?> GetDocumentsByUserAsync(string user);
    Task<Stream?> GetDownloadsAsync();
    Task<Stream?> GetDownloadByUserAsync(string user);
    Task<Stream?> GetNomenclatorByTypeAsync(int type);
}