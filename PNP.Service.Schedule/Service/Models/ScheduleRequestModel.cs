using System;
using System.Collections.Generic;

namespace PNP.Service.Schedule.Models
{
    public class ScheduleRequestModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SlotEntry TimeToAllocate { get; set; }
        public List<DateTime> IgnoreDates { get; set; } = new List<DateTime>();

    }
}
