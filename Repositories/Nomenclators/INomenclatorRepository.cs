using AicaDocsApi.Dto.Nomenclators;
using AicaDocsUI.Dto.Nomenclators;
using AicaDocsUI.Models;

namespace AicaDocsUI.Repositories.Nomenclators;

public interface INomenclatorRepository
{
    Task<IEnumerable<Nomenclator>?> GetNomenclatorsByTypeAsync(int type);
    Task<Nomenclator> GetNomenclatorAsync(int type, int id);
    Task CreateNomenclatorAsync(NomenclatorCreatedDto nomenclator);
    Task PatchNomenclatorAsync(int id, NomenclatorPatchDto name);
}