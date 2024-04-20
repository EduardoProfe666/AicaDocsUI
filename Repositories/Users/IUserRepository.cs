using AicaDocsApi.Dto.FilterCommons;
using AicaDocsUI.Models.Users;
using AicaDocsUI.Models.Users.Filter;

namespace AicaDocsUI.Repositories.Users;

public interface IUserRepository
{
    Task<UserDataDto?> GetUserData(string email);
    Task<FilterResponse<UserDataDto>?> GetUserDataFilter(FilterUserDto filter);
    Task<bool> DeleteUser(string email);
}