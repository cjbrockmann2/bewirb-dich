using CreepyApi.Domain;
using CreepyApi.DomainDto;
using CreepyApi.Infrastructure;
using CreepyApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CreepyApi.Controllers;

[Controller]
public class DokumenteController : Controller
{
  public Logger<DokumenteController> logger;
  private IDokumenteRepository _dokumentenService;

  public DokumenteController(ILoggerFactory loggerFactory, IDokumenteRepository dokumentenService)
  {
    logger = new Logger<DokumenteController>(loggerFactory);
    _dokumentenService = dokumentenService;
  }

  [HttpGet]
  [Route("/Dokumente")]
  public ActionResult<IEnumerable<DokumentenlisteEintragDto>> DokumenteAbrufen()
  {
    //var service = DokumenteService.Instance;
    var service = _dokumentenService;

    var okResult = new OkObjectResult(service.List().Select(dokument => MapToDto(dokument)));

    return okResult;
  }

  private DokumentenlisteEintragDto MapToDto(IDokument dokument)
  {
    return new DokumentenlisteEintragDto()
    {
      Id = dokument.Id,
      Beitrag = dokument.Beitrag,
      Berechnungsart = Enum.GetName(typeof(Berechnungsart), dokument.Berechnungsart)!,
      Dokumenttyp =  Enum.GetName(typeof(Dokumenttyp), dokument.Typ)!,
      Risiko = Enum.GetName(typeof(Risiko), dokument.Risiko)!,
      Versicherungssumme = dokument.Versicherungssumme,
      Zusatzschutz = $"{dokument.ZusatzschutzAufschlag}%",
      WebshopVersichert = dokument.HatWebshop,
      KannAngenommenWerden = !dokument.VersicherungsscheinAusgestellt && dokument.Typ == Dokumenttyp.Angebot,
      KannAusgestelltWerden = !dokument.VersicherungsscheinAusgestellt && dokument.Typ == Dokumenttyp.Versicherungsschein
    };
  }

  [HttpGet]
  [Route("/Dokumente/{id}")]
  public DokumentenlisteEintragDto DokumentFinden([FromRoute] Guid id)
  {
    // var dokument = DokumenteService.Instance.Find(id);
    var dokument = _dokumentenService.Find(id);

    if (dokument == null)
    {
      logger.LogError("Das Dokument mit der ID " + id + " konnte nicht gefunden werden.");
      throw new ArgumentNullException("Das Dokument mit der ID " + id + " konnte nicht gefunden werden.");
    }
    else
    {
      return MapToDto(dokument);
    }
  }

  [HttpPost]
  [Route("/Dokumente")]
  public ActionResult DokumenteErstellen([FromBody] ErzeugeNeuesAngebotDto dto)
  {
    //var service = DokumenteService.Instance;
    var service = _dokumentenService;

    if (dto.Versicherungssumme < 0)
    {
      throw new ArgumentOutOfRangeException("Die Versicherungssumme darf nicht negativ sein.");
    }

    if (string.IsNullOrWhiteSpace(dto.ZusatzschutzAufschlag))
    {
      dto.ZusatzschutzAufschlag = "0%";
    }

    if (dto.ZusatzschutzAufschlag.StartsWith("-"))
    {
      throw new ArgumentOutOfRangeException("Der Zusatzschutzaufschlag darf nicht negativ sein.");
    }

    var dokument = Factory.CreateDokumentFromDto(dto);

    service.Add(dokument);
    service.Save();

    return Ok();
  }

  [HttpPost]
  [Route("/Dokumente/{id}/annehmen")]
  public ActionResult DokumentAnnehmen([FromRoute] Guid id)
  {
    //var service = DokumenteService.Instance;
    var service = _dokumentenService;

    var dokument = service.Find(id);

    if (dokument == null)
    {
      logger.LogError("Das Dokument mit der ID " + id + " konnte nicht gefunden werden.");
      throw new ArgumentException("Das Dokument mit der Id " + id + " konnte nicht gefunden werden.");
    }

    dokument.Typ = Dokumenttyp.Versicherungsschein;
    service.Save();

    return new AcceptedResult();
  }

  [HttpPost]
  [Route("/Dokumente/{id}/ausstellen")]
  public ActionResult DokumentAusstellen([FromRoute] Guid id)
  {
    //var service = DokumenteService.Instance;
    var service = _dokumentenService;

    var dokument = service.Find(id);

    if (dokument == null)
    {
      logger.LogError("Das Dokument mit der ID " + id + " konnte nicht gefunden werden.");
      throw new ArgumentException("Das Dokument mit der Id " + id + " konnte nicht gefunden werden.");
    }

    if (dokument.Typ != Dokumenttyp.Versicherungsschein)
    {
      throw new ArgumentException("Nur ein Versicherungsschein kann ausgestellt werden.");
    }
    dokument.VersicherungsscheinAusgestellt = true;
    service.Save();

    return new AcceptedResult();
  }

}



