using System.Collections.Generic;

namespace ElQueue.BLL.Models
{
    public class UserBm
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public IEnumerable<TimeSlotBm> TimeSlots { get; set; }
    }
}
