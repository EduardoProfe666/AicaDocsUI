namespace AicaDocsUI.Repositories.Utils;

public interface IPaginationRepository
{
    Task<int?> GetCountPages(int type, int pageSize);
}