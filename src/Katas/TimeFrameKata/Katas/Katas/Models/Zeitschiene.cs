using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katas.Models
{
    public static class Zeitschiene
    {
        public static List<Zeitabschnitt> Zeitabschnitte { get; set; } = new List<Zeitabschnitt>();

        public static void Add(Zeitabschnitt abschnitt)
        {

            if (Zeitabschnitte.Count == 0) Zeitabschnitte.Add(abschnitt);
            else
            {
                var vorhanden = new List<Zeitabschnitt>(Zeitabschnitte.Where(
                        z => (abschnitt.Start >= z.Start && abschnitt.Start <= z.End) ||
                             (abschnitt.End >= z.Start && abschnitt.End <= z.End) ||
                             (abschnitt.Start <= z.Start && abschnitt.End >= z.End)
                        ));
                if (vorhanden.Count > 0)
                {

                    foreach (var z in vorhanden) Zeitabschnitte.Remove(z);
                    
                    vorhanden.Add(abschnitt);
                    var start = vorhanden.Min(x => x.Start);
                    var end = vorhanden.Max(x => x.End);

                    Zeitabschnitte.Add(new Zeitabschnitt(start, end));

                }
                else
                {
                    Zeitabschnitte.Add(abschnitt);
                }

            }
        }


    }
}
