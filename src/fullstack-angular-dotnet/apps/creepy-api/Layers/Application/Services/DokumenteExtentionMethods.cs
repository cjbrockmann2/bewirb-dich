using CreepyApi.Layers.Core.Enums;
using CreepyApi.Layers.Core.Models;

namespace CreepyApi.Layers.Application.Services;

public static class DokumenteExtentionMethods
{
  public static void Kalkuliere(this IDokument doc)
  {
    //Versicherungsnehmer, die nach Haushaltssumme versichert werden (primär Vereine) stellen immer ein mittleres Risiko da
    if (doc.Berechnungsart == Berechnungsart.Haushaltssumme)
    {
      doc.Risiko = Risiko.Mittel;
    }

    //Versicherungsnehmer, die nach Anzahl Mitarbeiter abgerechnet werden und mehr als 5 Mitarbeiter haben, können kein Lösegeld absichern
    if (doc.Berechnungsart == Berechnungsart.AnzahlMitarbeiter)
      if (doc.Berechnungbasis > 5)
      {
        doc.InkludiereZusatzschutz = false;
        doc.ZusatzschutzAufschlag = 0;
      }

    //Versicherungsnehmer, die nach Umsatz abgerechnet werden, mehr als 100.000€ ausweisen und Lösegeld versichern, haben immer mittleres Risiko
    if (doc.Berechnungsart == Berechnungsart.Umsatz)
      if (doc.Berechnungbasis > 100000m && doc.InkludiereZusatzschutz)
      {
        doc.Risiko = Risiko.Mittel;
      }

    decimal beitrag;
    switch (doc.Berechnungsart)
    {
      case Berechnungsart.Umsatz:
        var faktorUmsatz = (decimal)Math.Pow((double)doc.Versicherungssumme, 0.25d);
        beitrag = 1.1m + faktorUmsatz * (doc.Berechnungbasis / 100000);
        if (doc.HatWebshop) //Webshop gibt es nur bei Unternehmen, die nach Umsatz abgerechnet werden
          beitrag *= 2;
        break;
      case Berechnungsart.Haushaltssumme:
        var faktorHaushaltssumme = (decimal)Math.Log10((double)doc.Versicherungssumme);
        beitrag = 1.0m + faktorHaushaltssumme * doc.Berechnungbasis + 100m;
        break;
      case Berechnungsart.AnzahlMitarbeiter:
        var faktorMitarbeiter = doc.Versicherungssumme / 1000;

        if (doc.Berechnungbasis < 4)
          beitrag = faktorMitarbeiter + doc.Berechnungbasis * 250m;
        else
          beitrag = faktorMitarbeiter + doc.Berechnungbasis * 200m;

        break;
      default:
        throw new Exception();
    }

    if (doc.InkludiereZusatzschutz)
      beitrag *= 1.0m + (decimal)doc.ZusatzschutzAufschlag / 100.0m;

    if (doc.Risiko == Risiko.Mittel)
    {
      if (doc.Berechnungsart is Berechnungsart.Haushaltssumme or Berechnungsart.Umsatz)
        beitrag *= 1.2m;
      else
        beitrag *= 1.3m;
    }

    doc.Berechnungbasis = Math.Round(doc.Berechnungbasis, 2);
    doc.Beitrag = Math.Round(beitrag, 2);
  }


}
