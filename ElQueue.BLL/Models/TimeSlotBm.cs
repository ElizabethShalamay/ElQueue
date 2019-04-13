using ElQueue.DAL.Enums;
using System;

namespace ElQueue.BLL.Models
{
    public class TimeSlotBm
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public UserBm User { get; set; }

        public int QueueId { get; set; }

        public QueueBm Queue { get; set; }

        public DateTime TimeSlotStart { get; set; }

        public int Duration { get; set; }

        public QueueStatus Status { get; set; }
    }
}