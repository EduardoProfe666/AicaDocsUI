using AicaDocsUI.Repositories.ApiData.Dto.FilterCommons;
using AicaDocsUI.Repositories.ApiData.Dto.Users;
using AicaDocsUI.Repositories.ApiData.Dto.Users.Filter;

namespace AicaDocsUI.Repositories.Users;

public interface IUserRepository
{
    Task<UserDataDto?> GetUserData(string email);
    Task<FilterResponse<UserDataDto>?> GetUserDataFilter(FilterUserDto filter);
    Task<bool> DeleteUser(string email);
}