using System.ComponentModel.DataAnnotations;
using AicaDocsUI.Repositories.ApiData.Dto.Commons;
using Microsoft.Extensions.Options;

namespace AicaDocsUI.Repositories.ApiData.Dto.Downloads;

public class DownloadCreatedDto
{
    public required Format Format { get; set; }

    public required int DocumentId { get; set; }

    public required int ReasonId { get; set; }
    
}