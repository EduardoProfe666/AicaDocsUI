namespace AicaDocsUI.Utils.TokenServices;

public interface ITokenManager
{
    void SaveTokens(string accessToken, string refreshToken);
    void DeleteTokens();
    string? GetAccessToken();
    string? GetRefreshToken();
}