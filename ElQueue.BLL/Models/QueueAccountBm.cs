using ElQueue.DAL.Enums;

namespace ElQueue.BLL.Models
{
    public class QueueAccountBm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Details { get; set; }

        public QueueRating Rating { get; set; }
    }
}
