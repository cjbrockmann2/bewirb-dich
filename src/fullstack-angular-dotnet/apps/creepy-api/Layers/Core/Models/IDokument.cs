using CreepyApi.Layers.Core.Enums;

namespace CreepyApi.Layers.Core.Models;

public interface IDokument 
{
  Guid Id { get; }
  decimal Beitrag { get; set; }
  decimal Berechnungbasis { get; set; }
  Berechnungsart Berechnungsart { get; set; }
  bool HatWebshop { get; set; }
  bool InkludiereZusatzschutz { get; set; }
  Risiko Risiko { get; set; }
  Dokumenttyp Typ { get; set; }
  bool VersicherungsscheinAusgestellt { get; set; }
  decimal Versicherungssumme { get; set; }
  float ZusatzschutzAufschlag { get; set; }

}
