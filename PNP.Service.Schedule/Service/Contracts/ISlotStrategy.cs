using System;
using System.Collections.Generic;
using PNP.Service.Schedule.Models;

namespace PNP.Service.Schedule.Contracts
{
    public interface ISlotStrategy
    {
        List<DateTime> GetSlots(List<DateTime> timeEntries, SlotEntry entry);
    }
}
