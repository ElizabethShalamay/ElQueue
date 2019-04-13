using System;

namespace ElQueue.Orchestrator.Dtos
{
    public class NewQueueDto
    {
        public string Name { get; set; }

        public int AccountId { get; set; }

        public string Address { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int TimeSlotDuration { get; set; }

        public int TimeSlotNumber { get; set; }
    }
}
