using AicaDocsUI.Repositories.ApiData.Dto.Commons;

namespace AicaDocsUI.Repositories.ApiData.Dto.Downloads;

public class DownloadDto
{
    public int Id { get; set; }
    public DateTimeOffset DateOfDownload { get; set; } = DateTimeOffset.UtcNow;
    public required Format Format { get; set; }
    public required string UserEmail { get; set; }

    public required int DocumentId { get; set; }
    public required int ReasonId { get; set; }
}