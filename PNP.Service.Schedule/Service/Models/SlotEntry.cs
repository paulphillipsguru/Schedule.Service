using System;
using EWSoftware.PDI;

namespace PNP.Service.Schedule.Models
{
    public class SlotEntry
    {
        public RecurFrequency IntervalFrequency { get; set; }
        public int AllocateTime { get; set; }
        public RecurFrequency AllocateFrequency { get; set; }

        public int NumberOfSlots
        {
            get
            {
                float slots = 0;
                switch (IntervalFrequency)
                {
                    case RecurFrequency.Minutely:
                        switch (AllocateFrequency)
                        {
                            case RecurFrequency.Minutely:
                                slots = AllocateTime;
                                break;
                            case RecurFrequency.Hourly:
                                slots= 60 * AllocateTime;
                                break;
                            case RecurFrequency.Daily:
                                slots = 420 * AllocateTime;
                                break;
                        }

                        break;
                    case RecurFrequency.Hourly:
                        switch (AllocateFrequency)
                        {
                            case RecurFrequency.Minutely:
                                slots= 0.016666667f * AllocateTime;
                                break;
                            case RecurFrequency.Hourly:
                                slots = AllocateTime;
                                break;
                            case RecurFrequency.Daily:
                                slots = 0.142857143f * AllocateTime;
                                break;
                        }

                        break;
                    case RecurFrequency.Daily:
                        switch (AllocateFrequency)
                        {
                            case RecurFrequency.Minutely:
                                slots= AllocateTime / 420f;
                                break;
                            case RecurFrequency.Hourly:
                                slots= AllocateTime / 7f;
                                break;
                            case RecurFrequency.Daily:
                                slots= AllocateTime;
                                break;
                        }

                        break;

                }

                return (int)Math.Ceiling(slots);

            }
        }




    }
}
