using AicaDocsUI.Extensions;
using AicaDocsUI.Pages.PagesModelsData.Models.Downloads;
using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.Downloads;
using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;
using AicaDocsUI.Repositories.Auth;
using AicaDocsUI.Repositories.Downloads;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AicaDocsUI.Pages.Reports.Documents;

public class Download : PageModel
{
    private readonly INomenclatorRepository _nomenclatorRepository;
    private readonly IDownloadRepository _downloadRepository;
    private readonly IAuthRepository _authRepository;
    public IEnumerable<SelectListItem> ListFormat { get; set; }
    public IEnumerable<SelectListItem> ListReason { get; set; }

    [BindProperty] public DownloadCreatedModel DownloadCreatedModel { get; set; }
    [BindProperty] public IntegerWrapperValue Id11 { get; set; }

    public Download(INomenclatorRepository repository, IDownloadRepository downloadRepository, IAuthRepository authRepository)
    {
        _nomenclatorRepository = repository;
        _downloadRepository = downloadRepository;
        _authRepository = authRepository;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        bool b = await _authRepository.IsLoginAdvanceAsync();

        if (!b)
        {
            TempData["Unauthorized"] = true;
            return RedirectToPage("/Account/Login");
        }

        var c = await _authRepository.GetUserRoleAsync();
        if (c == null)
            return RedirectToPage();
        
        if (c != UserRole.Admin)
        {
            return RedirectToPage("/Error", new { code = 403 });
        }
        
        Id11 = new IntegerWrapperValue() { Id = id };
        DownloadCreatedModel = new DownloadCreatedModel
            { Format = Format.Pdf, DocumentId = id, ReasonId = 2 };

        ListFormat = Enum.GetValues(typeof(Format))
            .Cast<Format>()
            .Select(v => new SelectListItem
            {
                Text = v.GetDescription(),
                Value = v.ToString()
            });

        var data = await _nomenclatorRepository.GetNomenclatorsByTypeAsync((int)TypeOfNomenclator.ReasonOfDownload);

        ListReason = data!
            .Select(v => new SelectListItem
            {
                Text = v.Name,
                Value = v.Id.ToString()
            });
        return Page();
    }

    public async Task OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            DownloadCreatedModel.DocumentId = Id11.Id;
            var download = await _downloadRepository.DownloadDocumentAsync(new DownloadCreatedDto
            {
                Format = DownloadCreatedModel.Format, DocumentId = DownloadCreatedModel.DocumentId,
                ReasonId = DownloadCreatedModel.ReasonId
            });
            var indexUrl = Url.Page("./Index", new { downloadUrl = download, fileName = DownloadCreatedModel.Format==Format.Pdf ? "descarga.pdf" : "descarga.docx" });
            Response.Redirect(indexUrl!);

            //clear the Form
            ModelState.Clear();
        }
        else
        {
            ListFormat = Enum.GetValues(typeof(Format))
                .Cast<Format>()
                .Select(v => new SelectListItem
                {
                    Text = v.GetDescription(),
                    Value = v.ToString()
                });

            var data = await _nomenclatorRepository.GetNomenclatorsByTypeAsync((int)TypeOfNomenclator.ReasonOfDownload);

            ListReason = data!
                .Select(v => new SelectListItem
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                });
        }
    }

    public class IntegerWrapperValue
    {
        public int Id { get; set; }
    }
}