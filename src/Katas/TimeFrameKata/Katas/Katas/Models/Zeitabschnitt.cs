using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katas.Models
{
    public class Zeitabschnitt
    {
        public DateTime Start { get; private set; }

        public DateTime End { get; private set; }

        public static CultureInfo provider = new CultureInfo("de-DE");

        public Zeitabschnitt(DateTime start, DateTime end)
        {
            var newstart = new DateTime(start.Year, start.Month, start.Day, start.Hour, start.Minute, 0);
            var newend = new DateTime(end.Year, end.Month, end.Day, end.Hour, end.Minute, 0);

            if (newstart >= newend)
            {
                throw new ArgumentException("Der Anfangspunkt muss vor dem Endzeitpunkt liegen.");
            }

            Start = newstart;
            End = newend;

        }   

        public Zeitabschnitt(string a, string b) 
            : this(DateTime.Parse(a, new CultureInfo("de-DE"), DateTimeStyles.AssumeLocal)
                  , DateTime.Parse(b, new CultureInfo("de-DE"), DateTimeStyles.AssumeLocal))
        {
        }

        public override string  ToString()
        {
            var result = string.Empty;
            if (this.Start != null && this.End != null)
            {
                result = this.Start.ToString(provider)
                        + " - " + this.End.ToString(provider);
            }
            return result;
        }

    }
}
