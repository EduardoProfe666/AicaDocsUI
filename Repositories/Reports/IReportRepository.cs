namespace AicaDocsUI.Repositories.Reports;

public interface IReportRepository
{
    Task<Stream?> GetReportUsers();
    Task<Stream?> GetReportUserByRole(int role);
    Task<Stream?> GetDocuments();
    Task<Stream?> GetDocumentsByUser(string user);
    Task<Stream?> GetDownloads();
    Task<Stream?> GetDownloadByUser(string user);
    Task<Stream?> GetNomenclatorByType(int type);
}