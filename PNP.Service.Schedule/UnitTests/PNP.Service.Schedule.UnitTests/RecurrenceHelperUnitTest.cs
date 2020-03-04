
using System;
using PNP.Service.Schedule.Helpers;
using PNP.Service.Schedule.UnitTests.MockData;
using Xunit;

namespace PNP.Service.Schedule.UnitTests
{
    /// <summary>
    /// As we are using an External Library to manage Recurrence,
    /// we only need at this stage perform some basic tests.
    /// </summary>
    public class RecurrenceHelperUnitTest
    {
        [Fact]
        public void StandardWeek_ShouldReturn5Entries()
        {
            var recurrenceHelper = new RecurrenceHelper(ScheduleMockData.WeekDay9To11And13To17ByHour().Rule);
            var entries = recurrenceHelper.GetAll();
            Assert.NotNull(entries);
            Assert.True(entries.Count == 5, "There should be 5 entries");
        }

        [Fact]
        public void StandardWeek_MissingRule_ShouldThrowError()
        {
            Assert.Throws<ArgumentNullException>(()=> new RecurrenceHelper(""));
        }
    }
}
