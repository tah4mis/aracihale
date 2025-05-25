using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VehicleAuction.Web.TempModels;

public partial class TempDbContext : DbContext
{
    public TempDbContext()
    {
    }

    public TempDbContext(DbContextOptions<TempDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auction> Auctions { get; set; }

    public virtual DbSet<BankAccount> BankAccounts { get; set; }

    public virtual DbSet<Bid> Bids { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auction>(entity =>
        {
            entity.HasIndex(e => e.VehicleId, "IX_Auctions_VehicleId");

            entity.Property(e => e.MinimumIncrement).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StartingPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Auctions)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<BankAccount>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_BankAccounts_UserId");

            entity.Property(e => e.AccountHolder).HasMaxLength(100);
            entity.Property(e => e.AccountNumber).HasMaxLength(10);
            entity.Property(e => e.BankName).HasMaxLength(100);
            entity.Property(e => e.BranchCode).HasMaxLength(5);
            entity.Property(e => e.Iban)
                .HasMaxLength(26)
                .HasColumnName("IBAN");

            entity.HasOne(d => d.User).WithMany(p => p.BankAccounts).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Bid>(entity =>
        {
            entity.HasIndex(e => e.AuctionId, "IX_Bids_AuctionId");

            entity.HasIndex(e => e.UserId, "IX_Bids_UserId");

            entity.HasIndex(e => e.UserId1, "IX_Bids_UserId1");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Auction).WithMany(p => p.Bids).HasForeignKey(d => d.AuctionId);

            entity.HasOne(d => d.User).WithMany(p => p.BidUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.UserId1Navigation).WithMany(p => p.BidUserId1Navigations).HasForeignKey(d => d.UserId1);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Companies_UserId").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.AuthorizedPerson).HasMaxLength(100);
            entity.Property(e => e.AuthorizedPersonPhone).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(11);
            entity.Property(e => e.TaxNumber).HasMaxLength(10);

            entity.HasOne(d => d.User).WithOne(p => p.Company)
                .HasForeignKey<Company>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.CompanyId, "IX_Users_CompanyId");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(11);

            entity.HasOne(d => d.CompanyNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.Property(e => e.Abs).HasColumnName("ABS");
            entity.Property(e => e.Asr).HasColumnName("ASR");
            entity.Property(e => e.Esp).HasColumnName("ESP");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
