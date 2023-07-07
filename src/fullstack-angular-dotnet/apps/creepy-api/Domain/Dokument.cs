using CreepyApi.Controllers;
using CreepyApi.DomainDto;
using Newtonsoft.Json;
using System.Reflection.Metadata;

namespace CreepyApi.Domain;

public class Dokument : IDokument
{
  [JsonProperty]
  public Guid Id { get; private set; }

  public Dokumenttyp Typ { get; set; }
  public Berechnungsart Berechnungsart { get; set; }
  /// <summary>
  /// Berechnungsart Umsatz => Jahresumsatz in Euro,
  /// Berechnungsart Haushaltssumme => Haushaltssumme in Euro,
  /// Berechnungsart AnzahlMitarbeiter => Ganzzahl
  /// </summary>
  public decimal Berechnungbasis { get; set; }

  public bool InkludiereZusatzschutz { get; set; }
  public float ZusatzschutzAufschlag { get; set; }

  //Gibt es nur bei Unternehmen, die nach Umsatz abgerechnet werden
  public bool HatWebshop { get; set; }

  public Risiko Risiko { get; set; }

  public decimal Beitrag { get; set; }

  public bool VersicherungsscheinAusgestellt { get; set; }
  public decimal Versicherungssumme { get; set; }

  [System.Text.Json.Serialization.JsonConstructor]
  private Dokument()
  {

  }

  public Dokument(Guid guid)
  {
    this.Id = guid;
  }

  public void Kalkuliere()
  {
    //Versicherungsnehmer, die nach Haushaltssumme versichert werden (primär Vereine) stellen immer ein mittleres Risiko da
    if (this.Berechnungsart == Berechnungsart.Haushaltssumme)
    {
      this.Risiko = Risiko.Mittel;
    }

    //Versicherungsnehmer, die nach Anzahl Mitarbeiter abgerechnet werden und mehr als 5 Mitarbeiter haben, können kein Lösegeld absichern
    if (this.Berechnungsart == Berechnungsart.AnzahlMitarbeiter)
      if (this.Berechnungbasis > 5)
      {
        this.InkludiereZusatzschutz = false;
        this.ZusatzschutzAufschlag = 0;
      }

    //Versicherungsnehmer, die nach Umsatz abgerechnet werden, mehr als 100.000€ ausweisen und Lösegeld versichern, haben immer mittleres Risiko
    if (this.Berechnungsart == Berechnungsart.Umsatz)
      if (this.Berechnungbasis > 100000m && this.InkludiereZusatzschutz)
      {
        this.Risiko = Risiko.Mittel;
      }

    decimal beitrag;
    switch (this.Berechnungsart)
    {
      case Berechnungsart.Umsatz:
        var faktorUmsatz = (decimal)Math.Pow((double)this.Versicherungssumme, 0.25d);
        beitrag = 1.1m + faktorUmsatz * (this.Berechnungbasis / 100000);
        if (this.HatWebshop) //Webshop gibt es nur bei Unternehmen, die nach Umsatz abgerechnet werden
          beitrag *= 2;
        break;
      case Berechnungsart.Haushaltssumme:
        var faktorHaushaltssumme = (decimal)Math.Log10((double)this.Versicherungssumme);
        beitrag = 1.0m + faktorHaushaltssumme * this.Berechnungbasis + 100m;
        break;
      case Berechnungsart.AnzahlMitarbeiter:
        var faktorMitarbeiter = this.Versicherungssumme / 1000;

        if (this.Berechnungbasis < 4)
          beitrag = faktorMitarbeiter + this.Berechnungbasis * 250m;
        else
          beitrag = faktorMitarbeiter + this.Berechnungbasis * 200m;

        break;
      default:
        throw new Exception();
    }

    if (this.InkludiereZusatzschutz)
      beitrag *= 1.0m + (decimal)this.ZusatzschutzAufschlag / 100.0m;

    if (this.Risiko == Risiko.Mittel)
    {
      if (this.Berechnungsart is Berechnungsart.Haushaltssumme or Berechnungsart.Umsatz)
        beitrag *= 1.2m;
      else
        beitrag *= 1.3m;
    }

    this.Berechnungbasis = Math.Round(this.Berechnungbasis, 2);
    this.Beitrag = Math.Round(beitrag, 2);
  }


}


