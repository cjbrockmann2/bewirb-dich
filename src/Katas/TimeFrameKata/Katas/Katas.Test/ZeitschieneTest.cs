using Katas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katas.Test
{
    public class ZeitschieneTest
    {
        [Fact]
        public void Add_should_work() {
            // Arrange
            Zeitschiene.Zeitabschnitte = new List<Zeitabschnitt>();
            var zt = new Zeitabschnitt("1.1.2000", "1.1.2004");
            // Act 
            Zeitschiene.Add(zt);
            // Assert
            Assert.True(Zeitschiene.Zeitabschnitte.Count == 1);
        }

        [Fact]
        public void AddUeberlappende_should_work()
        {
            // Arrange
            Zeitschiene.Zeitabschnitte = new List<Zeitabschnitt>();
            var zt1 = new Zeitabschnitt("1.1.2000", "1.1.2004");
            var zt2 = new Zeitabschnitt("1.1.2001", "1.1.2005");
            // Act 
            Zeitschiene.Add(zt1);
            Zeitschiene.Add(zt2);
            // Assert
            Assert.True(Zeitschiene.Zeitabschnitte.Count == 1);
        }

        [Fact]
        public void AddUebergreifende_should_work()
        {
            // Arrange
            Zeitschiene.Zeitabschnitte = new List<Zeitabschnitt>();
            var zt1 = new Zeitabschnitt("1.1.2003", "1.1.2004");
            var zt2 = new Zeitabschnitt("1.1.2000", "1.1.2005");
            // Act 
            Zeitschiene.Add(zt1);
            Zeitschiene.Add(zt2);
            // Assert
            Assert.True(Zeitschiene.Zeitabschnitte.Count == 1);
        }


    }
}
