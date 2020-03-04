using System;
using PNP.Service.Schedule.Models;

namespace PNP.Service.Schedule.UnitTests.MockData
{
    public class ScheduleMockData
    {
        public static ScheduleModel WeekDay9To11And13To17ByHour()
        {
            return new ScheduleModel
            {
                Id = Guid.NewGuid(),
                Name = "Standard Week, Monday - Friday From 9am to 11pm and 1pm to 5pm",
                Rule = "FREQ=HOURLY;COUNT=5;BYDAY=MO,TU,WE,TH,FR;BYHOUR=9,10,11,13,14,15,16,17"
            };
        }
    }
}
