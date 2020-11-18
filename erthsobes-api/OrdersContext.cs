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

            builder.Entity<Order>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<Attachment>().Property<DateTime>("UpdatedTimestamp");

            base.OnModelCreating(builder);
        }

        public OrdersContext(DbContextOptions<OrdersContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            updateUpdatedProperty<Order>();
            updateUpdatedProperty<Attachment>();

            return base.SaveChanges();
        }

        private void updateUpdatedProperty<T>() where T : class
        {
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
            }
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("Host=172.23.0.4;Port=5432;Database=orders;Username=orders;Password=orders");
        //}
    }
}
