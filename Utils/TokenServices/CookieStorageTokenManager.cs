namespace AicaDocsUI.Utils.TokenServices;

public class CookieStorageTokenManager: ITokenManager
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CookieStorageTokenManager(IHttpContextAccessor httpContext)
    {
        _httpContextAccessor = httpContext;
    }

    public void SaveTokens(string accessToken, string refreshToken)
    {
        _httpContextAccessor.HttpContext!.Response.Cookies.Append(
            "accessToken",
            accessToken,
            new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(7),
                HttpOnly = true,
                IsEssential = true,
                SameSite = SameSiteMode.Strict
            }
        );
            
        _httpContextAccessor.HttpContext!.Response.Cookies.Append(
            "refreshToken",
            refreshToken,
            new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(7),
                HttpOnly = true,
                IsEssential = true,
                SameSite = SameSiteMode.Strict
            }
        );
    }

    public void DeleteTokens()
    {
        _httpContextAccessor.HttpContext!.Response.Cookies.Delete("accessToken", new CookieOptions
        {
            HttpOnly = true,
            IsEssential = true,
            SameSite = SameSiteMode.Strict
        });

        _httpContextAccessor.HttpContext!.Response.Cookies.Delete("refreshToken", new CookieOptions
        {
            HttpOnly = true,
            IsEssential = true,
            SameSite = SameSiteMode.Strict
        });
    }

    public string? GetAccessToken()
    {
        return _httpContextAccessor.HttpContext!.Request.Cookies["accessToken"];
    }

    public string? GetRefreshToken()
    {
        return _httpContextAccessor.HttpContext!.Request.Cookies["refreshToken"];
    }
    
    
    
}