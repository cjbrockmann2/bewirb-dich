using Autofac.Extras.Moq;
using CreepyApi.Layers.Application.Abstractions;
using CreepyApi.Layers.Core.Models;
using CreepyApi.Layers.Core.Enums;
using CreepyApi.Layers.Application.Services; 
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Castle.Components.DictionaryAdapter.Xml;
using Swashbuckle.AspNetCore.SwaggerUI;
using CreepyApi.Layers.Infrastructure;

namespace CreepyApi.Test;

public class DokumentenServiceTests
{

  private readonly DokumenteService _dokuService;
  private readonly Mock<ILoggerFactory> _loggerMock = new Mock<ILoggerFactory>(MockBehavior.Loose);
  private readonly Mock<IGenericRepository<IDokument>> _repoMock = new Mock<IGenericRepository<IDokument>>(MockBehavior.Loose);

  public DokumentenServiceTests()
  {
    //var mock = AutoMock.GetLoose();
    //_loggerMock = mock.Mock<ILoggerFactory>();
    //_repoMock = mock.Mock<IGenericRepository<IDokument>>();
    _dokuService = new DokumenteService(_loggerMock.Object, _repoMock.Object);
  }

  [Fact]
  public void DokumenteAbrufen_should_work()
  {
    // Arrange 
    var data = dokumentenListe();
    foreach (var item in data) item.Kalkuliere();
    _repoMock.Setup(x => x.GetAll()).Returns(data);
    var expected = data.ToList();

    // Act 
    var actual = _dokuService.DokumenteAbrufen().ToList();

    // Assert
    Assert.True(actual != null);
    Assert.Equal(expected.Count, actual.Count);
    for (int i = 0; i < expected.Count; i++)
    {
      Assert.Equal(expected[i].Beitrag, actual[i].Beitrag);
      Assert.Equal(expected[i].Berechnungbasis, actual[i].Berechnungbasis);
      Assert.Equal(expected[i].Id, actual[i].Id);
    }
  }

  [Fact]
  public void DokumenteErstellen_should_work()
  {
    // Arrange
    Dokument doc = (Dokument)dokumentenListe().ToList()[0];
    _repoMock.Setup(x => x.Save());
    var service = new DokumenteService(_loggerMock.Object, _repoMock.Object);
    var expected = new Dokument(doc.Id)
    {
      Berechnungsart = doc.Berechnungsart,
      Risiko = doc.Risiko,
      Versicherungssumme = doc.Versicherungssumme,
      HatWebshop = doc.HatWebshop,
      InkludiereZusatzschutz = doc.InkludiereZusatzschutz,
      ZusatzschutzAufschlag = doc.ZusatzschutzAufschlag
    }; 
    expected.Kalkuliere();

    // Act
    service.DokumenteErstellen(doc);

    // Assert
    _repoMock.Verify(x => x.Save(), Times.Exactly(1));

  }


  [Fact]
  public void DokumenteErstellen_Mehrere_should_work()
  {
    // Arrange
    var data = dokumentenListe();
    _repoMock.Setup(x => x.Save());
    _repoMock.Setup(x => x.dokumente).Returns(data.ToList());
    var service = new DokumenteService(_loggerMock.Object, _repoMock.Object);
    var expected = new List<IDokument>(data);
    foreach (var item in expected) item.Kalkuliere();

    // Act
    foreach(var doc in data)
    {
      service.DokumenteErstellen(doc);
    }
    // var repos = _repoMock.Object;

    // Assert
    _repoMock.Verify(x => x.Save(), Times.Exactly(expected.Count));

  }


  [Fact]
  public void DokumenteErstellen2_Mehrere_should_work()
  {
    using(var mock = AutoMock.GetLoose())
    {
      // Arrange
      mock.Mock<IGenericRepository<IDokument>>().Setup(x => x.Save());
      mock.Mock<IGenericRepository<IDokument>>().Setup(x => x.dokumente).Returns(dokumentenListe().ToList());
      mock.Mock<ILoggerFactory>();

      var liste = dokumentenListe().ToList();
      var serv = mock.Create<DokumenteService>();

      // Act 
      foreach (var item in liste)
      {
        serv.DokumenteErstellen(item);
      }

      Mock<IGenericRepository<IDokument>> repoMock = mock.Mock<IGenericRepository<IDokument>>();

      // Assert 
      repoMock.Verify(x => x.Save(), Times.Exactly(liste.Count));
       
      var repos = repoMock.Object;

    }
  }


  private IEnumerable<IDokument> dokumentenListe()
  {
    var list = new List<IDokument>
    {
      new Dokument(Guid.NewGuid())
      {
        Berechnungsart = Berechnungsart.Umsatz,
        Risiko = Risiko.Gering,
        Versicherungssumme = 10000,
        HatWebshop = true,
        InkludiereZusatzschutz = true,
        ZusatzschutzAufschlag = 20
      },
      new Dokument(Guid.NewGuid())
      {
        Berechnungsart = Berechnungsart.AnzahlMitarbeiter,
        Risiko = Risiko.Gering,
        Versicherungssumme = 20000,
        HatWebshop = true,
        InkludiereZusatzschutz = true,
        ZusatzschutzAufschlag = 25
      },
      new Dokument(Guid.NewGuid())
      {
        Berechnungsart = Berechnungsart.Haushaltssumme,
        Risiko = Risiko.Mittel,
        Versicherungssumme = 50000,
        HatWebshop = true,
        InkludiereZusatzschutz = true,
        ZusatzschutzAufschlag = 30
      }
    };

    
    
    return list;
  }

}
