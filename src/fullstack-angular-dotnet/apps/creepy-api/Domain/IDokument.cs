namespace CreepyApi.Domain
{
  public interface IDokument
  {
    decimal Beitrag { get; set; }
    decimal Berechnungbasis { get; set; }
    Berechnungsart Berechnungsart { get; set; }
    bool HatWebshop { get; set; }
    Guid Id { get; }
    bool InkludiereZusatzschutz { get; set; }
    Risiko Risiko { get; set; }
    Dokumenttyp Typ { get; set; }
    bool VersicherungsscheinAusgestellt { get; set; }
    decimal Versicherungssumme { get; set; }
    float ZusatzschutzAufschlag { get; set; }

    void Kalkuliere();
  }
}