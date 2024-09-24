using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.ReservationApp.Models
{
    public class ReservationContext : DbContext
    {
        public ReservationContext(DbContextOptions<ReservationContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationTable> ReservationTables { get; set; }
        public DbSet<PaymentRecord> PaymentRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Replace with your actual SQL Server connection string
            //optionsBuilder.UseSqlServer(@"Server=(LocalDb)\LocalDB;Database=ReservationDb_v2;Trusted_Connection=True;");
            optionsBuilder.UseSqlServer(@"Data Source=nusiss.cpku8mwo022g.ap-southeast-1.rds.amazonaws.com;Initial Catalog=NUSISS;User ID=admin;Password=eUbHxTz67UQjN3Rd3liD;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ReservationTable>()
        //        .HasKey(rt => new { rt.ReservationID, rt.TableID });

        //    modelBuilder.Entity<ReservationTable>()
        //        .HasOne(rt => rt.Reservation)
        //        .WithMany(r => r.ReservationTables)
        //        .HasForeignKey(rt => rt.ReservationID);

        //    modelBuilder.Entity<ReservationTable>()
        //        .HasOne(rt => rt.Table)
        //        .WithMany(t => t.ReservationTables)
        //        .HasForeignKey(rt => rt.TableID);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Reservations)
                .WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerID);
        }
    }
}

