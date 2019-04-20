using System;
using System.Collections.Generic;
using ElQueue.DAL.Enums;
using ElQueue.DAL.Models;

namespace ElQueue.BLL.Services.TimeSlotService
{
    public class TimeSlotService : ITimeSlotService
    {
        public IEnumerable<TimeSlot> GetTimeSlots(DateTime start, DateTime end, int duration, int queueId)
        {
            var totalTime = (end - start).TotalMinutes;
            var availableSlots = (int)(totalTime / duration);

            return CalculateSlots(start, availableSlots, duration, queueId);
        }

        private IEnumerable<TimeSlot> CalculateSlots(DateTime start, int availableSlots, int duration, int queueId)
        {
            var timeSlots = new List<TimeSlot>();
            var timeSlotStart = start;

            for(int i = 0; i < availableSlots; i++)
            {
                timeSlots.Add(new TimeSlot
                {
                    TimeSlotStart = timeSlotStart,
                    Duration = duration,
                    Status = QueueStatus.FreeTimeSlot,
                    QueueId = queueId
                });

                timeSlotStart = timeSlotStart.AddMinutes(duration);
            }

            return timeSlots;
        }
    }
}
