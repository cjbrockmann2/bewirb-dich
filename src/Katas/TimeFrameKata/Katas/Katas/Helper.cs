using Katas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katas
{
    public static class Helper
    {
        public static string IntegerToString(int value)
        {
            var blanks = "    ";
            int pos = blanks.Length - (int) Math.Log10(value);
            if (pos < 0) pos = 0;
            return blanks.Substring(0,pos) + value;
        }

        public static void ZeitschieneAusgeben(List<Zeitabschnitt> zeitschiene, string titel = "")
        {
            zeitschiene = zeitschiene.OrderBy(x => x.Start).ToList();
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine(titel);
            Console.WriteLine("       Start                 Ende");
            for (int i = 0; i < zeitschiene.Count; i++)
            {
                Console.WriteLine(Helper.IntegerToString(i + 1) + ": " + zeitschiene[i].ToString());
            }

        }


        public static void InputAusgeben(List<(string, string)> tuppelListe, string titel = "")
        {
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine(titel);
            Console.WriteLine("       Start                 Ende");
            for (int i = 0; i < tuppelListe.Count; i++)
            {
                try
                {
                    var von = DateTime.Parse(tuppelListe[i].Item1, Zeitabschnitt.provider).ToString();
                    var bis = DateTime.Parse(tuppelListe[i].Item2, Zeitabschnitt.provider).ToString();
                    Console.WriteLine(Helper.IntegerToString(i + 1) + ": " + von + " - " + bis);
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }

        private class ZeitAbschnittViewModel  
        {
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public DateOnly Datum { get; set; }
            public override string ToString()
            {
                var result = string.Empty;
                if (this.Start != null && this.End != null)
                {
                    result = this.Start.ToString(Zeitabschnitt.provider)
                            + " - " + this.End.ToString(Zeitabschnitt.provider);
                }
                return result;
            }


        }

        public static void ZeitschieneGruppiertAusgeben(List<Zeitabschnitt> zeitschiene, string titel = "")
        {
            var zeitschiene2 = zeitschiene.OrderBy(x => x.Start)
                .Select(x => new ZeitAbschnittViewModel { Start = x.Start, End = x.End, Datum = new DateOnly(x.Start.Year, x.Start.Month, x.Start.Day) }) 
                .ToList();

            IEnumerable<IGrouping<DateOnly, ZeitAbschnittViewModel>> zeitschieneGruppiert = zeitschiene2.GroupBy(x => x.Datum);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine(titel);
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

            int i = 0;
            foreach(IGrouping<DateOnly, ZeitAbschnittViewModel> grp in zeitschieneGruppiert)
            {
                Console.WriteLine("Datum: " + grp.Key.ToString());
                Console.WriteLine("       Start                 Ende");
                foreach(var g in grp)
                {
                    Console.WriteLine(Helper.IntegerToString(i + 1) + ": " + g.ToString());
                    i++;
                }
                Console.WriteLine("------------------------------------------------------------------");
            }


        }


    }
}
