using AicaDocsUI.Repositories.Auth;
using AicaDocsUI.Repositories.Reports;

namespace AicaDocsUI.Extensions;

public static class DownloadReportEndpoint
{
    public static void MapDownloadReportEndpoint(this WebApplication app)
    {
        app.MapGet("/downloadReport/{type}/{number}/{user}", async(TypeReport type, int number, string user, IAuthRepository auth, IReportRepository rp,CancellationToken ct) =>
        {
            if (!await auth.IsLoginAdvance()) return Results.Unauthorized();
            
            Stream? stream;
            string namePdf;
            switch (type)
            {
                case TypeReport.Users:
                    namePdf = "Reporte de usuarios";
                    stream = await rp.GetReportUsers();
                    break;
                case TypeReport.UsersRoles:
                    namePdf = "Reporte de usuarios de un rol";
                    stream = await rp.GetReportUserByRole(number);
                    break;
                case TypeReport.Documents:
                    namePdf = "Reporte de documentos";
                    stream = await rp.GetDocuments();
                    break;
                case TypeReport.DocumentsByUser:
                    namePdf = "Reporte de documentos de un usuario";
                    stream = await rp.GetDocumentsByUser(user);
                    break;
                case TypeReport.Downloads:
                    namePdf = "Reporte de descargas";
                    stream = await rp.GetDownloads();
                    break;
                case TypeReport.DownloadsByUser:
                    namePdf = "Reporte de descargas de un usuario";
                    stream = await rp.GetDownloadByUser(user);
                    break;
                case TypeReport.NomenclatorsByType:
                    namePdf = "Reporte de un tipo adicional";
                    stream = await rp.GetNomenclatorByType(number);
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