using System;
namespace PNP.Service.Schedule.Models
{
    public class ScheduleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Rule { get; set; }
    }
}
