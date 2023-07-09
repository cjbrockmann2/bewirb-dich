using CreepyApi.Domain;
using System.Globalization;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace CreepyApi.Helper;

public static class ParsingHelper
{
  public static Risiko ParseRisiko(string risiko)
  {
    if (string.IsNullOrEmpty(risiko)) risiko = string.Empty;
    risiko = risiko.Trim().ToLower();
    switch (risiko)
    {
      case "gering": return Risiko.Gering;
      case "mittel": return Risiko.Mittel;
      case "": throw new ArgumentException("Es wurde kein Wert für Risiko angegeben"); ;
      default: throw new ArgumentException($"'{risiko}' ist kein gültiger Wert");
    }
  }

  public static decimal PruefeVersicherungssumme(decimal versicherungssumme)
  {
    decimal result = 0;
    if (versicherungssumme <= 0) throw new ArgumentException("Die Versicherungssumme muss größer als null sein");
    else result = versicherungssumme; 
    return result;
  }


  public static float ParseZusatzaufschlag(string zusatzaufschlag)
  {
    float result = 0;
    zusatzaufschlag = zusatzaufschlag.Replace("%", "").Trim();
    float.TryParse(zusatzaufschlag, out result);
    if (result < 0) throw new ArgumentException("Der Zusatzaufschlag darf nicht negativ sein!");
    return result;
  }

  public static Berechnungsart ParseBerechnungsart(string berechnungsart)
  {
    if (string.IsNullOrEmpty(berechnungsart)) berechnungsart = string.Empty;
    berechnungsart = berechnungsart.Trim().ToLower();
    switch (berechnungsart)
    {
      case "umsatz": return Berechnungsart.Umsatz;
      case "haushaltssumme": return Berechnungsart.Haushaltssumme;
      case "anzahlmitarbeiter": return Berechnungsart.AnzahlMitarbeiter;
      case "": throw new ArgumentException("Die Berechnungsart fehlt");
      default: throw new ArgumentException($"'{berechnungsart}' ist kein gültiger Wert");
    }
  }

}
