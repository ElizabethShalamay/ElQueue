using System;

namespace ElQueue.Orchestrator.Dtos
{
    public class TimeSlotTemplate
    {
        public int QueueId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int TimeSlotDuration { get; set; }

        public int TimeSlotNumber { get; set; }
    }
}
