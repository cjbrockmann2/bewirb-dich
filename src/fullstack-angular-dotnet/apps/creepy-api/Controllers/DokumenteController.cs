using CreepyApi.Domain;
using CreepyApi.DomainDto;
using CreepyApi.Infrastructure;
using CreepyApi.Layers.Application.Services;
using CreepyApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CreepyApi.Controllers;

[Controller]
public class DokumenteController : Controller
{
  public Logger<DokumenteController> logger;
  private IGenericRepository<IDokument> _repo;
  private IDokumenteService _service;

  public DokumenteController(
      ILoggerFactory loggerFactory,
      IGenericRepository<IDokument> repo,
      IDokumenteService service)
  {
    logger = new Logger<DokumenteController>(loggerFactory);
    _repo = repo;
    _service = service;
  }

  [HttpGet]
  [Route("/Dokumente")]
  public ActionResult<IEnumerable<DokumentenlisteEintragDto>> DokumenteAbrufen()
  {
    var result = _service.DokumenteAbrufen();

    var okResult = new OkObjectResult(result.Select(dokument => MapToDto(dokument)).ToList());

    return okResult;
  }

  private DokumentenlisteEintragDto MapToDto(IDokument dokument)
  {
    return new DokumentenlisteEintragDto()
    {
      Id = dokument.Id,
      Beitrag = dokument.Beitrag,
      Berechnungsart = Enum.GetName(typeof(Berechnungsart), dokument.Berechnungsart)!,
      Dokumenttyp = Enum.GetName(typeof(Dokumenttyp), dokument.Typ)!,
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
    var dokument = _service.DokumentFinden(id);
    return MapToDto(dokument);
  }

  [HttpPost]
  [Route("/Dokumente")]
  public ActionResult DokumenteErstellen([FromBody] ErzeugeNeuesAngebotDto dto)
  {
    var dokument = Factory.CreateDokumentFromDto(dto);
    bool b = _service.DokumenteErstellen(dokument);

    return Ok();
  }

  [HttpPost]
  [Route("/Dokumente/{id}/annehmen")]
  public ActionResult DokumentAnnehmen([FromRoute] Guid id)
  {
    var b = _service.DokumentAnnehmen(id);
    return new AcceptedResult();
  }

  [HttpPost]
  [Route("/Dokumente/{id}/ausstellen")]
  public ActionResult DokumentAusstellen([FromRoute] Guid id)
  {
    var b = _service.DokumentAusstellen(id);
    return new AcceptedResult();
  }

}



