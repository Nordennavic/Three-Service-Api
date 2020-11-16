using System;
using System.Linq;
using System.Threading.Tasks;
using erthsobes_api.Model;
using Microsoft.EntityFrameworkCore;

namespace erthsobes_api
{
    public class OrdersContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            builder.Entity<Order>().HasKey(m => m.id);
            builder.Entity<Attachment>().HasKey(m => m.id);
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public OrdersContext(DbContextOptions<OrdersContext> options) :base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=172.23.0.4;Port=5432;Database=orders;Username=orders;Password=orders");
        }
    }
}
