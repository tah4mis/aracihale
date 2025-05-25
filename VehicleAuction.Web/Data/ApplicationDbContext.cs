using Microsoft.EntityFrameworkCore;
using VehicleAuction.Web.Models;

namespace VehicleAuction.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Vehicle configurations
            modelBuilder.Entity<Vehicle>()
                .Property(v => v.Price)
                .HasColumnType("decimal(18,2)");

            // Auction configurations
            modelBuilder.Entity<Auction>()
                .Property(a => a.StartingPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Auction>()
                .Property(a => a.MinimumIncrement)
                .HasColumnType("decimal(18,2)");

            // Bid configurations
            modelBuilder.Entity<Bid>()
                .Property(b => b.Amount)
                .HasColumnType("decimal(18,2)");

            // Company configurations
            modelBuilder.Entity<Company>()
                .HasIndex(c => c.Name)
                .IsUnique();

            // BankAccount configurations
            modelBuilder.Entity<BankAccount>()
                .Property(b => b.Balance)
                .HasColumnType("decimal(18,2)");
        }
    }
} 