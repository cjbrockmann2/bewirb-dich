using CreepyApi.Domain;

namespace CreepyApi.Infrastructure;

public interface IDokumenteRepository
{
    IDokument? Find(Guid id);
    List<IDokument> List();
    void Add(IDokument dokument);
    void Save();
}
