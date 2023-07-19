using System.Reflection;
using System.Text;
using System.Text.Json;
using CreepyApi.Controllers.Dto;
using CreepyApi.Helper;
using CreepyApi.Layers.Application.Abstractions;
using CreepyApi.Layers.Core.Enums;
using CreepyApi.Layers.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CreepyApi.Layers.Application.Services;


public class DokumenteService : IDokumenteService
{
  public Logger<DokumenteService> logger;
  private IGenericRepository<IDokument> _repo;

  public DokumenteService(ILoggerFactory loggerFactory, IGenericRepository<IDokument> repo)
  {
    logger = new Logger<DokumenteService>(loggerFactory);
    _repo = repo;
  }


  public IEnumerable<IDokument> DokumenteAbrufen()
  {
    var result = _repo.GetAll().ToList();

    return result;
  }

  public IDokument DokumentFinden(Guid id)
  {
    // var dokument = DokumenteService.Instance.Find(id);
    var dokument = _repo.Get(id);

    if (dokument == null)
    {
      logger.LogError("Das Dokument mit der ID " + id + " konnte nicht gefunden werden.");
      throw new ArgumentNullException("Das Dokument mit der ID " + id + " konnte nicht gefunden werden.");
    }
    else
    {
      return dokument;
    }
  }

  public bool DokumenteErstellen(IDokument doc)
  {
    //var _repo = DokumenteService.Instance;
    bool result = false;

    if (doc.Versicherungssumme < 0)
    {
      throw new ArgumentOutOfRangeException("Die Versicherungssumme darf nicht negativ sein.");
    }

    if (doc.ZusatzschutzAufschlag < 0)
    {
      throw new ArgumentOutOfRangeException("Der Zusatzschutzaufschlag darf nicht negativ sein.");
    }

    doc.Kalkuliere();

    _repo.Add(doc);
    _repo.Save();

    result = true;

    return result;
  }

  public bool DokumentAnnehmen(Guid id)
  {
    //var _repo = DokumenteService.Instance;
    var dokument = _repo.Get(id);

    if (dokument == null)
    {
      logger.LogError("Das Dokument mit der ID " + id + " konnte nicht gefunden werden.");
      throw new ArgumentException("Das Dokument mit der Id " + id + " konnte nicht gefunden werden.");
    }

    dokument.Typ = Dokumenttyp.Versicherungsschein;
    _repo.Save();

    return true;
  }

  public bool DokumentAusstellen(Guid id)
  {
    var dokument = _repo.Get(id);

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
    _repo.Save();

    return true;
  }




}

