using erthsobesapi.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace erthsobesapi
{
    public class OrdersContext : DbContext
    {
        public DbSet<Order> Order_info { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>().HasKey(m => m.id);
            builder.Entity<Attachment>().HasKey(m => m.id);
            builder.Entity<Attachment>()
                .Property(m => m.hash).IsRequired();
            base.OnModelCreating(builder);
        }

        public OrdersContext(DbContextOptions<OrdersContext> options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
