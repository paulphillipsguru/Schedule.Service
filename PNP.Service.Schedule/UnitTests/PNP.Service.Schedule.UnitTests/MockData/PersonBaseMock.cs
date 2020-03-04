using System;
using PNP.Service.Schedule.Models;

namespace PNP.Service.Schedule.UnitTests.MockData
{
    public class PersonMock : ResourceModel
    {
        public PersonMock()
        {
            Id = Guid.NewGuid();
            Name = "Paul Phillips";
            DefaultSchedule = ScheduleMockData.WeekDay9To11And13To17ByHour();
        }
    }
}
