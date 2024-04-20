using AicaDocsApi.Dto.FilterCommons;
using AicaDocsUI.Models.IdentityCommons;

namespace AicaDocsUI.Models.Users.Filter;

public class FilterUserDto
{
    public required string? Email { get; set; }
    public required string? Fullname { get; set; }
    public required UserRole? Role { get; set; }

    public required bool NotIncludeThisAccount { get; set; }

    public PaginationParams PaginationParams { get; set; } = new PaginationParams();
    public SortByUser SortBy { get; set; } = SortByUser.Id;
    public SortOrder SortOrder { get; set; } = SortOrder.Asc;
}