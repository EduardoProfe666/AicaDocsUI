using AicaDocsUI.Extensions;
using AicaDocsUI.Pages.PagesModelsData.Models.Downloads;
using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using AicaDocsUI.Repositories.ApiData.Dto.Downloads;
using AicaDocsUI.Repositories.Downloads;
using AicaDocsUI.Repositories.Nomenclators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AicaDocsUI.Pages.Documents;

public class Download : PageModel
{
    private readonly INomenclatorRepository _nomenclatorRepository;
    private readonly IDownloadRepository _downloadRepository;
    public IEnumerable<SelectListItem> ListFormat { get; set; }
    public IEnumerable<SelectListItem> ListReason { get; set; }

    [BindProperty] public DownloadCreatedModel DownloadCreatedModel { get; set; }
    [BindProperty] public IntegerWrapperValue Id11 { get; set; }

    public Download(INomenclatorRepository repository, IDownloadRepository downloadRepository)
    {
        _nomenclatorRepository = repository;
        _downloadRepository = downloadRepository;
    }

    public async Task OnGetAsync(int id)
    {
        Id11 = new IntegerWrapperValue(){Id = id};
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