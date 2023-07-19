using Microsoft.AspNetCore.Mvc;
using CreepyApi.Controllers.Dto;
using CreepyApi.Layers.Core.Enums;

namespace CreepyApi.Controllers;

public class KonfigurationsdatenController : Controller
{
  [HttpGet]
  [Route("/Config/Berechnungsarten")]
  public IEnumerable<ConfigTypeDto>? Berechnungsarten()
  {
    return getFromEnum<Berechnungsart>(); 
  }

  [HttpGet]
  [Route("/Config/Dokumententypen")]
  public IEnumerable<ConfigTypeDto>? Dokumententypen()
  {
    return getFromEnum<Dokumenttyp>();
  }

  [HttpGet]
  [Route("/Config/Risikoarten")]
  public IEnumerable<ConfigTypeDto>? Risikoarten()
  {
    return getFromEnum<Risiko>();
  }


  /***
   * Diese Methode setzt voraus, dass der Enum-Type eindeutige Integer-Values hat... 
   */
  private IEnumerable<ConfigTypeDto>? getFromEnum<T>() where T : Enum
  {
    var t = typeof(T); 
    var typeList = Enum.GetValues(t)
      .Cast<int>()
      .Select(e =>
          new ConfigTypeDto {
              Id = e,
              Name = Enum.GetName(t, e) 
          }
      ).Where(x => !string.IsNullOrEmpty(x.Name));
    return typeList;
  }


}



