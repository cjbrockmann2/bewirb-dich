using CreepyApi.Domain;

namespace CreepyApi.Layers.Application.Services
{
  public interface IDokumenteService
  {
    bool DokumentAnnehmen(Guid id);
    bool DokumentAusstellen(Guid id);
    IEnumerable<IDokument> DokumenteAbrufen();
    bool DokumenteErstellen(IDokument doc);
    IDokument DokumentFinden(Guid id);
  }
}
