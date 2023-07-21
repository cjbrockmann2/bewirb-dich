using CreepyApi.Domain;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace CreepyApi.Infrastructure;


public class DokumentRepository : IGenericRepository<IDokument>
{
  private static readonly string JSONPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/dokumente.json";

  public List<IDokument> dokumente { get; private set; } = new List<IDokument>();

  public DokumentRepository()
  {
    // Console.WriteLine(this.GetType());
    var docs = LoadDokumenteFromJSON();
    if (docs != null) dokumente = docs;
  }

  private static List<IDokument> LoadDokumenteFromJSON()
  {
    if (!File.Exists(JSONPath))
    {
      var empty = Enumerable.Empty<DokumentDAL>();
      File.WriteAllText(JSONPath, JsonSerializer.Serialize(empty), Encoding.UTF8);
    }

    var json = File.ReadAllText(JSONPath, Encoding.UTF8);
    var opt = new JsonSerializerOptions();
    opt.IncludeFields = true;
    opt.PropertyNameCaseInsensitive = false;
    var result = JsonSerializer.Deserialize<List<DokumentDAL>>(json, opt) ?? new List<DokumentDAL>();
    return result.Select(x => toDokument(x)).ToList<IDokument>();
  }

  public IDokument? Get(Guid id)
  {
    return dokumente.Find(x => x.Id == id);
  }

  public IEnumerable<IDokument> GetAll()
  {
    dokumente = LoadDokumenteFromJSON();
    return dokumente;
  }

  public void Add(IDokument dokument)
  {
    dokumente.Add(dokument);
  }

  public void Save()
  {
    var dokumenteDAL = dokumente.Select(x => toDokumentDAL(x)).ToList<DokumentDAL>();
    var json = JsonSerializer.Serialize(dokumenteDAL);
    File.WriteAllText(JSONPath, json, new UTF8Encoding());
  }

  public IEnumerable<IDokument> Find(Func<IDokument, bool> func)
  {
    var result = dokumente.Where(func).ToList();
    return result;
  }

  public void Remove(IDokument entity)
  {
    var item = dokumente.Find(x => x == entity);
    if (item != null)
    {
      dokumente.Remove(item);
    }
  }

  public static DokumentDAL toDokumentDAL(IDokument source)
  {
    var result = new DokumentDAL()
    {
      Id = source.Id,
      Typ = source.Typ.ToString(),
      Berechnungsart = source.Berechnungsart.ToString(),
      Berechnungbasis = source.Berechnungbasis,
      InkludiereZusatzschutz = source.InkludiereZusatzschutz,
      ZusatzschutzAufschlag = source.ZusatzschutzAufschlag,
      HatWebshop = source.HatWebshop,
      Risiko = source.Risiko.ToString(),
      Beitrag = source.Beitrag,
      VersicherungsscheinAusgestellt = source.VersicherungsscheinAusgestellt,
      Versicherungssumme = source.Versicherungssumme
    };
    return result;
  }


  public static IDokument toDokument(DokumentDAL source)
  {
    var result = new Dokument(source.Id)
    {
      Typ = Enum.Parse<Dokumenttyp>(source.Typ, true),
      Berechnungsart = Enum.Parse<Berechnungsart>(source.Berechnungsart, true),
      Berechnungbasis = source.Berechnungbasis,
      InkludiereZusatzschutz = source.InkludiereZusatzschutz,
      ZusatzschutzAufschlag = source.ZusatzschutzAufschlag,
      HatWebshop = source.HatWebshop,
      Risiko = Enum.Parse<Risiko>(source.Risiko, true),
      Beitrag = source.Beitrag,
      VersicherungsscheinAusgestellt = source.VersicherungsscheinAusgestellt,
      Versicherungssumme = source.Versicherungssumme
    };
    return result;
  }


  public class DokumentDAL
  {
    public Guid Id { get; set; }

    public string Typ { get; set; }
    public string Berechnungsart { get; set; }
    /// <summary>
    /// Berechnungsart Umsatz => Jahresumsatz in Euro,
    /// Berechnungsart Haushaltssumme => Haushaltssumme in Euro,
    /// Berechnungsart AnzahlMitarbeiter => Ganzzahl
    /// </summary>
    public decimal Berechnungbasis { get; set; }

    public bool InkludiereZusatzschutz { get; set; }
    public float ZusatzschutzAufschlag { get; set; }

    //Gibt es nur bei Unternehmen, die nach Umsatz abgerechnet werden
    public bool HatWebshop { get; set; }

    public string Risiko { get; set; }

    public decimal Beitrag { get; set; }

    public bool VersicherungsscheinAusgestellt { get; set; }
    public decimal Versicherungssumme { get; set; }
  }


}


