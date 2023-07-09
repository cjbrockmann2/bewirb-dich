using CreepyApi.Domain;
using CreepyApi.DomainDto;
using CreepyApi.Helper;

namespace CreepyApi.Services;

public static class Factory
{
  public static IDokument CreateDokument()
  {
    var guid = Guid.NewGuid();
    var dokument = new Dokument(guid);
    return dokument;
  }


  public static IDokument CreateDokumentFromDto(ErzeugeNeuesAngebotDto dto)
  {
    var dokument = CreateDokument();
    dokument.InkludiereZusatzschutz = dto.WillZusatzschutz;
    dokument.HatWebshop = dto.HatWebshop;
    dokument.VersicherungsscheinAusgestellt = false;
    dokument.Risiko = ParsingHelper.ParseRisiko(dto.Risiko);
    dokument.Versicherungssumme = ParsingHelper.PruefeVersicherungssumme(dto.Versicherungssumme);
    dokument.ZusatzschutzAufschlag = ParsingHelper.ParseZusatzaufschlag(dto.ZusatzschutzAufschlag);
    dokument.Typ = Dokumenttyp.Angebot;
    dokument.Berechnungsart = ParsingHelper.ParseBerechnungsart(dto.Berechnungsart);
    dokument.Kalkuliere();
    return dokument;
  }



}
