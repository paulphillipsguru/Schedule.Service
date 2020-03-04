using System;
using System.Collections.Generic;
using PNP.Service.Schedule.Contracts;
using PNP.Service.Schedule.Models;

namespace PNP.Service.Schedule.Strategies
{
    public class ContinuousSlotStrategy : ISlotStrategy
    {
        public List<DateTime> GetSlots(List<DateTime> timeEntries, SlotEntry entry)
        {
            if (entry.AllocateTime > timeEntries.Count)
            {
                //We cannot allocate time as we require more slots that 
                //what is available.
                return new List<DateTime>();
            }
            return timeEntries.GetRange(0, entry.NumberOfSlots); ;
        }
    }
}
