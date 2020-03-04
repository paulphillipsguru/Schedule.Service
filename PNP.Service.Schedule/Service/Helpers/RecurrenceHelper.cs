
using System;
using System.Collections.Generic;
using System.Linq;
using EWSoftware.PDI;

namespace PNP.Service.Schedule.Helpers
{
    public class RecurrenceHelper
    {
        
        private readonly Recurrence _recurrence;

        public RecurFrequency Frequency => _recurrence.Frequency;

        public RecurrenceHelper(string rule)
        {
            if (string.IsNullOrWhiteSpace(rule))
            {
                throw  new ArgumentNullException("rule param cannot be null");
            }

            _recurrence = new Recurrence(rule);

        }

        public List<DateTime> GetAll()
        {
            return _recurrence.AllInstances().ToList();
        }


        public List<DateTime> RemoveLeave(List<DateTime> dates, List<DateTime> leave)
        {
            return dates.Where(p => !leave.Contains(p.Date)).ToList();
        }
        public List<DateTime> GetTimesBetweenTwoDates(DateTime startDate, DateTime endDate)
        {
            if (endDate <= startDate)
            {
                throw new ArgumentOutOfRangeException("endDate cannot start before startDate");
            }
           

            _recurrence.StartDateTime = startDate;
            _recurrence.RecurUntil = endDate;

            return _recurrence.AllInstances().ToList();
        }


    }
}
