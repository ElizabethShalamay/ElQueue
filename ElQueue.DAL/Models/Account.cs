using ElQueue.DAL.Enums;
using System.Collections.Generic;

namespace ElQueue.DAL.Models
{
    public class Account
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string  Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Details { get; set; }

        public QueueRating Rating { get; set; }

        public IEnumerable<Queue> Queues { get; set; }
    }
}
