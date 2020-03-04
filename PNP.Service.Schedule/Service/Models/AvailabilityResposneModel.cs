using System;
using System.Collections.Generic;
using System.Linq;

namespace PNP.Service.Schedule.Models
{
    public class AvailabilityResponseModel
    {
        public IList<DateTime> AvailableDateTimes { get; set; }

        public bool CanAllocate => AvailableDateTimes.Any();
   
      
    }
}
