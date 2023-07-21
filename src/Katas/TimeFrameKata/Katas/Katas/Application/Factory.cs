using Katas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katas.Application
{
    public static class Factory
    {
        public static Zeitabschnitt? GetZeitabschnitt(string von, string bis) {
            try
            {
                return new Zeitabschnitt(von, bis);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
