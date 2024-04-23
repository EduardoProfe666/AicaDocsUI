using AicaDocsUI.Repositories.ApiData.Dto.Nomenclators;

namespace AicaDocsUI.Repositories.Nomenclators;

public interface INomenclatorRepository
{
    Task<IEnumerable<NomenclatorDto>?> GetNomenclatorsByTypeAsync(int type);
    Task<NomenclatorDto?> GetNomenclatorAsync(int type, int id);
    Task<bool> CreateNomenclatorAsync(NomenclatorCreatedDto nomenclator);
    Task<bool> PutNomenclatorAsync(int id, NomenclatorPutDto name);

    Task<bool> DeleteNomenclatorAsync(int type, int id);
}