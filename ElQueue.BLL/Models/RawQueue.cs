namespace ElQueue.BLL.Models
{
    public class RawQueue
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public int AccountId { get; set; }

        public QueueAccountBm Account { get; set; }

        public string Address { get; set; }
    }
}
