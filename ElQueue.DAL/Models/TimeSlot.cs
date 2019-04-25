using ElQueue.DAL.Enums;
using System;

namespace ElQueue.DAL.Models
{
    public class TimeSlot
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int QueueId { get; set; }

        public Queue Queue { get; set; }

        public DateTime TimeSlotStart { get; set; }

        public int Duration { get; set; }

        public QueueStatus Status { get; set; }
    }
}
