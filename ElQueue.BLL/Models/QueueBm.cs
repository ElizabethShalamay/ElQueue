using System;
using System.Collections.Generic;
using System.Text;

namespace ElQueue.BLL.Models
{
    public class QueueBm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public int AccountId { get; set; }

        public QueueAccountBm Account { get; set; }

        public string Address { get; set; }

        public IEnumerable<TimeSlotBm> TimeSlots { get; set; }
    }
}
