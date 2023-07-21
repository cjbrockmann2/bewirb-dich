using Katas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katas.Test
{
    public class ZeitabschnittTest
    {
        [Theory]
        [InlineData("1.1.2023 12:00", "1.1.2023 13:00")]
        public void ZeitabschnittErstellen_should_work(string von, string bis)
        {
            // Arrange
            // Act 
            var zt = new Zeitabschnitt(von, bis);
            // Assert 
            Assert.True(zt != null);
        }


        [Theory]
        [InlineData("1.1.2023 12:00", "1.1.2023 7:00")]
        [InlineData("1.1.2023 12:00", "1.1.2023 12:00")]
        public void ZeitabschnittErstellen_should_fail(string von, string bis)
        {
            // Arrange
            // Act & Assert             
            Assert.Throws<ArgumentException>(() => new Zeitabschnitt(von, bis));
        }

    }



}
