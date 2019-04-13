using System;

namespace ElQueue.Orchestrator.QueueOrchestrator
{
    public class QueueToUpdateDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AccountId { get; set; }

        public string Address { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int TimeSlotDuration { get; set; }

        public int TimeSlotNumber { get; set; }
    }
}