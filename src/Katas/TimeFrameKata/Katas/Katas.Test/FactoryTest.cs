using Katas.Application;
using Katas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katas.Test
{
    public class FactoryTest
    {


        [Theory]
        [InlineData("1.1.2023", "3.1.2023")]
        [InlineData("1.1.2023 12:00", "1.1.2023 13:00")]
        [InlineData("1.1.2023 12:00:59", "1.1.2023 13:00:59")]
        public void GetZeitabschnitt_should_return_data(string von, string bis)
        {
            // Arrange
            var vonDt = DateTime.Parse(von);
            var bisDt = DateTime.Parse(bis);
            // Act
            var zeit = Factory.GetZeitabschnitt(von, bis);
            // Assert             
            Assert.True(zeit != null);
            Assert.True(CompareDateTimes(vonDt, zeit.Start));
            Assert.True(CompareDateTimes(bisDt, zeit.End));
        }


        [Theory]
        [InlineData("1.1.2023", "31.12.2022")]
        [InlineData("1.1.2023 12:00:59", "1.1.2023 12:00:00")]
        [InlineData("1.1.2023 12:00:00", "1.1.2023 12:00:00")]
        public void GetZeitabschnitt_should_return_null(string von, string bis)
        {
            // Arrange
            // Act
            var zeit = Factory.GetZeitabschnitt(von, bis);
            // Assert             
            Assert.True(zeit == null);
        }

        private bool CompareDateTimes(DateTime dt1, DateTime dt2)
        {
            if (
                dt1.Year == dt2.Year &&
                dt1.Month == dt2.Month &&
                dt1.Day == dt2.Day &&
                dt1.Hour == dt2.Hour &&
                dt1.Minute == dt2.Minute 
                ) return true;
            else return false;
        }

    }
}
