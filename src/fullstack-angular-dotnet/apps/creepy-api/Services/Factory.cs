using CreepyApi.Domain;
using CreepyApi.DomainDto;

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
    dokument.Risiko = RisikoHelper.Parse(dto.Risiko);
    dokument.Versicherungssumme = dto.Versicherungssumme;
    dokument.ZusatzschutzAufschlag = float.Parse(dto.ZusatzschutzAufschlag.Replace("%", ""));
    dokument.Typ = Dokumenttyp.Angebot;
    dokument.Berechnungsart = BerechnungsartHelper.Parse(dto.Berechnungsart);
    dokument.Kalkuliere();
    return dokument;
  }



}
