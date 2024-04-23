using System.ComponentModel;

namespace AicaDocsUI.Repositories.ApiData.Dto.IdentityCommons;

public enum UserRole
{
    [Description("Administrador")]
    Admin,
    [Description("Trabajador")]
    Worker
}