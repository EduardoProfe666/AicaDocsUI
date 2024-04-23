using AicaDocsUI.Repositories.ApiData.Dto.FilterCommons;
using AicaDocsUI.Repositories.ApiData.Dto.Users;
using AicaDocsUI.Repositories.ApiData.Dto.Users.Filter;

namespace AicaDocsUI.Repositories.Users;

public interface IUserRepository
{
    Task<UserDataDto?> GetUserDataAsync(string email);
    Task<FilterResponse<UserDataDto>?> GetUserDataFilterAsync(FilterUserDto filter);
    Task<bool> DeleteUserAsync(string email);
}