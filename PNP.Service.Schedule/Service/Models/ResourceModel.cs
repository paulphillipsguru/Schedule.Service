using System;
using System.Collections.Generic;

namespace PNP.Service.Schedule.Models
{
    public class ResourceModel
    {
        public Guid Id { get; set; }
        public ScheduleModel DefaultSchedule { get; set; }
        public List<DateTime> Leave { get; set; } = new List<DateTime>();
        public string Name { get; set; }
    }
}
