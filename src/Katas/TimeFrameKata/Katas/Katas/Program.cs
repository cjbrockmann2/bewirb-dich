// See https://aka.ms/new-console-template for more information

using Katas;
using Katas.Application;
using Katas.Models;
using System.Globalization;
using System.Threading.Tasks.Sources;

 

List<(string, string)> list = new List<(string, string)>();
list.Add(("01.01.2023 7:30", "1.1.2023 7:40"));
list.Add(("01.01.2023 8:30", "1.1.2023 8:40"));
list.Add(("01.01.2023 9:30", "1.1.2023 9:40"));
list.Add(("01.02.2023 8:30:51", "01.02.2023 12:51:52"));
list.Add(("01.03.2023 8:30:51", "01.03.2023 12:51:52"));
list.Add(("01.04.2023 8:30:51", "01.04.2023 12:51:52"));
list.Add(("01.05.2023 8:30:51", "01.05.2023 12:51:52"));
list.Add(("01.06.2023 8:30:51", "01.06.2023 12:51:52"));
list.Add(("01.07.2023 8:30:51", "01.07.2023 12:51:52"));
list.Add(("01.08.2023 8:30:51", "01.08.2023 12:51:52"));
list.Add(("01.09.2023 8:30:51", "01.09.2023 12:51:52"));
list.Add(("01.10.2023 8:30:51", "01.10.2023 12:51:52"));

// Überschneidende Zeitabschnitte
list.Add(("01.02.2023 7:00", "01.02.2023 18:00"));
list.Add(("01.03.2023 7:00", "01.02.2023 9:00"));
list.Add(("01.04.2023 7:00", "01.09.2023 19:00"));

// Fehlerhafte Zeitabschnitte
list.Add(("01.02.2023 7:00", "01.02.2023 6:00"));
list.Add(("", ""));


foreach (var z in list)
{
    var abschnitt = Factory.GetZeitabschnitt(z.Item1, z.Item2);
    if (abschnitt != null) Zeitschiene.Add(abschnitt);
}


Console.Clear();
Console.WriteLine("KATAS!");
Console.WriteLine();


Helper.InputAusgeben(list, "Input");
Helper.ZeitschieneAusgeben(Zeitschiene.Zeitabschnitte, "Output");
Helper.ZeitschieneGruppiertAusgeben(Zeitschiene.Zeitabschnitte, "Output gruppiert");

Console.WriteLine();
Console.WriteLine("Bitte eine Taste drücken zum Beenden...");
Console.ReadKey();
