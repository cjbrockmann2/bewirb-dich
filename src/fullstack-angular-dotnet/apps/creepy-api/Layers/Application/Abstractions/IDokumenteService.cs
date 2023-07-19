using CreepyApi.Layers.Core.Models;

namespace CreepyApi.Layers.Application.Abstractions
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