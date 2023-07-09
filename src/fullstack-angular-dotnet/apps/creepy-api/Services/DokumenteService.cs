using System.Reflection;
using System.Text;
using System.Text.Json;
using CreepyApi.Domain;
using CreepyApi.Infrastructure;

namespace CreepyApi.Services;

public class DokumenteService : IGenericRepository<IDokument>
{
    private static readonly string JSONPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/dokumente.json";
    
    private List<IDokument> dokumente = new List<IDokument>();

    public DokumenteService()
    {
      Console.WriteLine(this.GetType());
    }

    private static List<IDokument> LoadDokumenteFromJSON()
    {
        if (!File.Exists(JSONPath))
        {
            var empty = Enumerable.Empty<IDokument>();
            File.WriteAllText(JSONPath, JsonSerializer.Serialize(empty), Encoding.UTF8);
        }

        var json = File.ReadAllText(JSONPath, Encoding.UTF8);
        return JsonSerializer.Deserialize<List<IDokument>>(json) ?? new List<IDokument>();
    }

    public IDokument? Find(Guid id)
    {
        foreach (var dokument in dokumente)
        {
            if (dokument.Id == id)
            {
                return dokument;
            }
        }

        return null;
    }

    public List<IDokument> List()
    {
        return dokumente.ToList();
    }

    public void Add(IDokument dokument)
    {
        dokumente.Add(dokument);
    }

    public void Save()
    {
        var json = JsonSerializer.Serialize(dokumente);
        File.WriteAllText(JSONPath, json, new UTF8Encoding());
    }
}   
