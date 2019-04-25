using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElQueue.DAL.Models
{
    [Table("Users")]
    public class User : IdentityUser
    {
        public string Address { get; set; }

        public IEnumerable<TimeSlot> TimeSlots { get; set; }
    }
}
