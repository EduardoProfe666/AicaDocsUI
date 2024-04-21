using AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;
using AicaDocsUI.Repositories.Auth;
using AicaDocsUI.Repositories.Reports;

namespace AicaDocsUI.Pages.Reports;

public static class DownloadReportEndpoint
{
    public static void MapDownloadReportEndpoint(this WebApplication app)
    {
        app.MapGet("/downloadReport/{type}/{number}/{user}", async(TypeReport type, int number, string user, IAuthRepository auth, IReportRepository rp,CancellationToken ct) =>
        {
            if (!await auth.IsLoginAdvanceAsync()) return Results.Unauthorized();
            UserRole? ur = await auth.GetUserRoleAsync();

            if (ur is null or UserRole.Worker && type is TypeReport.Downloads or TypeReport.Users
                    or TypeReport.NomenclatorsByType or TypeReport.UsersRoles or TypeReport.DocumentsByUser
                    or TypeReport.DownloadsByUser)
                return Results.Forbid();
            
            Stream? stream;
            string namePdf;
            switch (type)
            {
                case TypeReport.Users:
                    namePdf = "Reporte de usuarios";
                    stream = await rp.GetReportUsersAsync();
                    break;
                case TypeReport.UsersRoles:
                    namePdf = "Reporte de usuarios de un rol";
                    stream = await rp.GetReportUserByRoleAsync(number);
                    break;
                case TypeReport.Documents:
                    namePdf = "Reporte de documentos";
                    stream = await rp.GetDocumentsAsync();
                    break;
                case TypeReport.DocumentsByUser:
                    namePdf = "Reporte de documentos de un usuario";
                    stream = await rp.GetDocumentsByUserAsync(user);
                    break;
                case TypeReport.Downloads:
                    namePdf = "Reporte de descargas";
                    stream = await rp.GetDownloadsAsync();
                    break;
                case TypeReport.DownloadsByUser:
                    namePdf = "Reporte de descargas de un usuario";
                    stream = await rp.GetDownloadByUserAsync(user);
                    break;
                case TypeReport.NomenclatorsByType:
                    namePdf = "Reporte de un tipo adicional";
                    stream = await rp.GetNomenclatorByTypeAsync(number);
                    break;
                default:
                    return Results.BadRequest();
            }
            
            return stream is not null ? Results.File(stream, "application/pdf", $"{namePdf}.pdf") : Results.NotFound();
        });
    }

    public enum TypeReport
    {
        Users,
        UsersRoles,
        Documents,
        DocumentsByUser,
        Downloads,
        DownloadsByUser,
        NomenclatorsByType
    }

}