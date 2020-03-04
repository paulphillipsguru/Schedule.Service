using System;
using EWSoftware.PDI;
using PNP.Service.Schedule.Logic;
using PNP.Service.Schedule.Models;
using PNP.Service.Schedule.Strategies;
using PNP.Service.Schedule.UnitTests.MockData;
using Xunit;

namespace PNP.Service.Schedule.UnitTests
{

    public class AvailabilityManagerUnitTest
    {
        [Fact]
        public void CanAllocate_AvailableWeekDay_ShouldReturnTrue()
        {
            var paulTheAuditor = new PersonMock();
            var standardSlotStrategy = new ContinuousSlotStrategy();
            var availManager = new AvailabilityManager(paulTheAuditor, standardSlotStrategy);
            var request = new ScheduleRequestModel
            {
                StartDate = new DateTime(2020, 03, 02),
                EndDate = new DateTime(2020, 03, 06),
                TimeToAllocate = new SlotEntry
                {
                    AllocateTime = 15,
                    AllocateFrequency = RecurFrequency.Hourly
                }
            };

            var availability =
                availManager.GetAvailability(request);

            Assert.True(availability.CanAllocate, "The resource should be available");
        }

        [Fact]
        public void CanAllocate_AvailableWeekend_ShouldReturnFalse()
        {
            var paulTheAuditor = new PersonMock();
            var standardSlotStrategy = new ContinuousSlotStrategy();
            var availManager = new AvailabilityManager(paulTheAuditor, standardSlotStrategy);
            var request = new ScheduleRequestModel
            {
                StartDate = new DateTime(2020, 03, 07),
                EndDate = new DateTime(2020, 03, 08),
                TimeToAllocate = new SlotEntry
                {
                    AllocateTime = 15,
                    AllocateFrequency = RecurFrequency.Hourly
                }
            };

            var availability =
                availManager.GetAvailability(request);

            Assert.False(availability.CanAllocate, "The resource should not be available");
        }

        [Fact]
        public void CanAllocate_MustAllocate15Hours_ShouldReturnTrue()
        {
            var paulTheAuditor = new PersonMock();
            var standardSlotStrategy = new ContinuousSlotStrategy();
            var availManager = new AvailabilityManager(paulTheAuditor, standardSlotStrategy);
            var request = new ScheduleRequestModel
            {
                StartDate = new DateTime(2020, 03, 02),
                EndDate = new DateTime(2020, 03, 06),
                TimeToAllocate = new SlotEntry
                {
                    AllocateTime = 15,
                    AllocateFrequency = RecurFrequency.Hourly
                }
            };

            var availability =
                availManager.GetAvailability(request);

            Assert.True(availability.CanAllocate, "The resource should be available");

        }
        [Fact]
        public void CanAllocate_MustAllocate500Hours_ShouldReturnFalse()
        {
            var paulTheAuditor = new PersonMock();
            var standardSlotStrategy = new ContinuousSlotStrategy();
            var availManager = new AvailabilityManager(paulTheAuditor, standardSlotStrategy);
            var request = new ScheduleRequestModel
            {
                StartDate = new DateTime(2020, 03, 02),
                EndDate = new DateTime(2020, 03, 06),
                TimeToAllocate = new SlotEntry
                {
                    AllocateTime = 500,
                    AllocateFrequency = RecurFrequency.Hourly
                }
            };

            var availability =
                availManager.GetAvailability(request);

            Assert.False(availability.CanAllocate, "The resource should not be available");

        }

        [Fact]
        public void CanAllocate_AllocateTimeWithBlockedLeave_ShouldReturnFalse()
        {
            var paulTheAuditor = new PersonMock();
            paulTheAuditor.Leave.Add(new DateTime(2020,03,02));
            paulTheAuditor.Leave.Add(new DateTime(2020, 03, 03));
            paulTheAuditor.Leave.Add(new DateTime(2020, 03, 04));
            paulTheAuditor.Leave.Add(new DateTime(2020, 03, 05));
            paulTheAuditor.Leave.Add(new DateTime(2020, 03, 06));
            var standardSlotStrategy = new ContinuousSlotStrategy();
            var availManager = new AvailabilityManager(paulTheAuditor, standardSlotStrategy);
            var request = new ScheduleRequestModel
            {
                StartDate = new DateTime(2020, 03, 02),
                EndDate = new DateTime(2020, 03, 06),
                TimeToAllocate = new SlotEntry
                {
                    AllocateTime = 10,
                    AllocateFrequency = RecurFrequency.Hourly
                }
            };

            var availability =
                availManager.GetAvailability(request);

            Assert.False(availability.CanAllocate, "The resource should not be available because the resource is on holiday");

        }


        [Fact]
        public void CanAllocate_AllocateTimeWithBlockedDates_ShouldReturnTrue()
        {
            var paulTheAuditor = new PersonMock();
        
            var standardSlotStrategy = new ContinuousSlotStrategy();
            var availManager = new AvailabilityManager(paulTheAuditor, standardSlotStrategy);
            var request = new ScheduleRequestModel
            {
                
                StartDate = new DateTime(2020, 03, 02),
                EndDate = new DateTime(2020, 03, 06),
                TimeToAllocate = new SlotEntry
                {
                    AllocateTime = 10,
                    AllocateFrequency = RecurFrequency.Hourly,

                }
            };
            request.IgnoreDates.Add(new DateTime(2020,03,03));

            var availability =
                availManager.GetAvailability(request);

            Assert.True(availability.CanAllocate, "The resource should be able to allocate time");

        }
    }
}
