using System;
using System.Linq;
using PNP.Service.Schedule.Contracts;
using PNP.Service.Schedule.Helpers;
using PNP.Service.Schedule.Models;

namespace PNP.Service.Schedule.Logic
{
    /// <summary>
    /// AvailabilityManager prime goal is determine available for a particular
    /// resource.
    /// Works with Minutes, Hours and Day time slot.
    /// The resource can define their own frequency of slots, e.g. if frequency is Hourly
    /// and the Request Frequency is Minutes then there needs to be 1 hour slots available for
    /// every 60 minutes requested.
    /// 
    /// </summary>
    /// <example>
    ///     Resource
    ///     ---------------
    ///     Name: Paul The Auditor
    ///     Available Mon through to Friday
    ///     Hours are between 9 and 11am and 1pm and 5pm. (Schedule:WeekDay9To11And13To17ByHour)
    ///     The schedule use RRULE format:FREQ=HOURLY;COUNT=5;BYDAY=MO,TU,WE,TH,FR;BYHOUR=9,10,11,13,14,15,16,17
    ///
    ///     Workflow:
    ///     1. Generate all available slots between two dates using the RRULE ignoring public holidays
    ///     2. Remove leave for the resource
    ///     3. From the slots, get the first X slows based on frequency for the resource and the request
    ///     
    ///     Slots generated for Resource on a hourly basis
    ///     03/03/2020 09:00
    ///     03/03/2020 10:00
    ///     03/03/2020 11:00
    ///     03/03/2020 13:00
    ///     03/03/2020 14:00
    ///     03/03/2020 15:00
    ///     03/03/2020 16:00
    ///     03/03/2020 17:00
    ///
    ///     Requested 1 Day to be booked, the all the above would be returned
    /// </example>
    /// <remarks>
    /// Terms:
    /// SLOT = A Unit of time.  This depends on the frequency of the resource.
    ///     A slot could be a minute, hour or a single day  
    /// AvailabilityManager is not concerned with:
    /// 1. Storage
    /// 2. Bookings / Calender
    /// </remarks>
    public class AvailabilityManager
    {
        public ResourceModel Resource { get; private set; }
        private ISlotStrategy _slotStrategy;
        public AvailabilityManager(ResourceModel resource, ISlotStrategy strategy)
        {

            _slotStrategy = strategy ?? throw new ArgumentNullException("strategy cannot be null");
            Resource = resource ?? throw new ArgumentNullException("resource cannot be null");
            if (Resource.DefaultSchedule == null)
            {
                throw new NullReferenceException("Resource Default Schedule has not been set");
            }
        }

        public AvailabilityResponseModel GetAvailability(ScheduleRequestModel request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request cannot be null");
            }

            if (request.StartDate == null)
            {
                throw new ArgumentNullException("startDate is required");
            }

            if (request.EndDate == null)
            {
                throw new ArgumentNullException("endDate is required");
            }

            if (request.TimeToAllocate == null)
            {
                throw new ArgumentNullException("timeToAllocate is required");
            }

            var resourceHelper = new RecurrenceHelper(Resource.DefaultSchedule.Rule);

            //Lets get all available time slots for a resource exclude public holidays and leave.
            var availableDates = resourceHelper.GetTimesBetweenTwoDates(request.StartDate, request.EndDate);

            if (Resource.Leave != null && Resource.Leave.Any())
            {
                //Remove Leave
                availableDates = resourceHelper.RemoveLeave(availableDates, Resource.Leave);
            }

            if (request.IgnoreDates != null && request.IgnoreDates.Any())
            {
                //Remove Leave
                availableDates = resourceHelper.RemoveLeave(availableDates, request.IgnoreDates);
            }



            if (availableDates.Any())
            {
                //Now we have the available complete dates, lets now filter down by 
                //time slots.
                request.TimeToAllocate.IntervalFrequency = resourceHelper.Frequency;
                availableDates = _slotStrategy.GetSlots(availableDates, request.TimeToAllocate);
            }


            var response = new AvailabilityResponseModel
            {
                AvailableDateTimes = availableDates
            };

            return response;

        }

    }
}
