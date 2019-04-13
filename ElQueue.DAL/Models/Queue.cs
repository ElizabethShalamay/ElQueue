namespace ElQueue.DAL.Models
{
    public class Queue
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public int AccountId { get; set; }

        public Account Account { get; set; }

        public string Address { get; set; }
    }
}
