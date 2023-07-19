
using CreepyApi.Controllers.Dto;
using CreepyApi.Helper;
using CreepyApi.Layers.Core.Enums;
using CreepyApi.Layers.Core.Models;

namespace CreepyApi.Layers.Application.Services;

public static class Factory
{
  public static Dokument CreateDokument()
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
    return dokument;
  }



}
