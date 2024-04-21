using AicaDocsUI.Repositories.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AicaDocsUI.Pages.Account;

public class Logout : PageModel
{
    private readonly IAuthRepository _authRepository;

    public Logout(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public IActionResult OnGet()
    {
        _authRepository.Logout();
        return LocalRedirect("/");
    }
    
    public IActionResult OnPost()
    {
        _authRepository.Logout();
        return LocalRedirect("/");
    }
}