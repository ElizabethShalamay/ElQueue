using ElQueue.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ElQueue.DAL.Infrastructure
{
    public class QueueContext : IdentityDbContext<User>
    {
        public QueueContext(DbContextOptions<QueueContext> options) : base(options)
        { }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Queue> Queues { get; set; }

        public DbSet<TimeSlot> TimeSlots { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<User>().HasIndex(p => p.UserName).IsUnique();
            modelBuilder.Entity<Queue>().HasIndex(g => g.Name).IsUnique();

            modelBuilder.Entity<Queue>().HasOne(queue => queue.Account)
                .WithMany(g => g.Queues).Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<TimeSlot>().HasOne(timeSlot => timeSlot.User)
                .WithMany(user => user.TimeSlots).Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<TimeSlot>().HasOne(timeSlot => timeSlot.Queue)
                .WithMany(queue => queue.TimeSlots).Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
        }

    }
}
