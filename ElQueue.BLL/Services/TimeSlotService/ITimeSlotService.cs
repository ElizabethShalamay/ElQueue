using ElQueue.DAL.Models;
using System;
using System.Collections.Generic;

namespace ElQueue.BLL.Services.TimeSlotService
{
    public interface ITimeSlotService
    {
        IEnumerable<TimeSlot> GetTimeSlots(DateTime start, DateTime end, int duration, int queueId);
    }
}
