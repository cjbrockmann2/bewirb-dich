namespace CreepyApi.Domain;

public enum Risiko
{
    Gering = 1,
    Mittel = 2
}

public static class RisikoHelper
{
    public static Risiko Parse(string risiko)
    {
      if (string.IsNullOrEmpty(risiko)) risiko = string.Empty;
      risiko = risiko.Trim().ToLower();
      switch (risiko)
      {
          case "gering": return Risiko.Gering;
          case "mittel": return Risiko.Mittel;
          default: throw new ArgumentException($"{risiko} ist kein gültiger Wert");
      }
    }
}
