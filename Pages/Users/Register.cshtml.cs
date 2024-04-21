using AicaDocsUI.Extensions;
using AicaDocsUI.Repositories.ApiData.Dto.Auth;
using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.Documents;
using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;
using AicaDocsUI.Repositories.ApiData.Dto.Users;
using AicaDocsUI.Repositories.Auth;
using AicaDocsUI.Repositories.Documents;
using AicaDocsUI.Repositories.Nomenclators;
using AicaDocsUI.Repositories.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AicaDocsUI.Pages.Users;

public class Register : PageModel
{
    private readonly IAuthRepository _authRepository;

    public IEnumerable<SelectListItem> ListRoles { get; set; } = new List<SelectListItem>();

    [BindProperty] public RegisterRequestModel RegisterRequestModel { get; set; }

    public Register(IAuthRepository repository)
    {
        _authRepository = repository;
    }

    public void OnGet(int id)
    {
        RegisterRequestModel = new RegisterRequestModel()
        {
            FullName = "", Email = "", Role = 0
        };

        ListRoles = Enum.GetValues(typeof(UserRole))
            .Cast<UserRole>()
            .Select(v => new SelectListItem
            {
                Text = v.GetDescription(),
                Value = v.ToString()
            });
    }

    public async Task OnPostAsync()
    {
        
        if (ModelState.IsValid)
        {
            var register = await _authRepository.Register(RegisterRequestModel.Email, RegisterRequestModel.FullName,
                RegisterRequestModel.Role);

            TempData["Created User"] = register;
            TempData["User Exists"] = !register;
            
            Response.Redirect("/Users/Index");

            //clear the Form
            ModelState.Clear();
        }
        else
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            
            ListRoles = Enum.GetValues(typeof(UserRole))
                .Cast<UserRole>()
                .Select(v => new SelectListItem
                {
                    Text = v.GetDescription(),
                    Value = v.ToString()
                });
        }
    }
}