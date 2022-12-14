using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Wallet
{
    public partial class WalletContext : DbContext
    {
        public WalletContext()
        {
        }

        public WalletContext(DbContextOptions<WalletContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Coin> Coins { get; set; } = null!;
        public virtual DbSet<Currency> Currencies { get; set; } = null!;
        public virtual DbSet<Exchange> Exchanges { get; set; } = null!;
        public virtual DbSet<History> Histories { get; set; } = null!;
        public virtual DbSet<ListOfCoin> ListOfCoins { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Wallet; Integrated security=true");
            }
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coin>(entity =>
            {
                entity.HasKey(e => e.IdCoins);

                entity.HasIndex(e => e.IdCurrency, "IX_Coins_IdCryptoCurrency");

                entity.Property(e => e.NumberOfCoins).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdCurrencyNavigation)
                    .WithMany(p => p.Coins)
                    .HasForeignKey(d => d.IdCurrency)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Coins_CryptoCurrency");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(e => e.IdCurrency)
                    .HasName("PK_CryptoCurrency");

                entity.ToTable("Currency");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("money");
            });

            modelBuilder.Entity<Exchange>(entity =>
            {
                entity.HasKey(e => e.IdExchange);

                entity.ToTable("Exchange");

                entity.HasIndex(e => e.IdGetCurrency, "IX_Exchange_IdGetCurrency");

                entity.HasIndex(e => e.IdGiveCurrency, "IX_Exchange_IdListOfCoins");

                entity.Property(e => e.Size).HasColumnType("money");

                entity.HasOne(d => d.IdGetCurrencyNavigation)
                    .WithMany(p => p.ExchangeIdGetCurrencyNavigations)
                    .HasForeignKey(d => d.IdGetCurrency)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exchange_Currency1");

                entity.HasOne(d => d.IdGiveCurrencyNavigation)
                    .WithMany(p => p.ExchangeIdGiveCurrencyNavigations)
                    .HasForeignKey(d => d.IdGiveCurrency)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exchange_Currency");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.HasKey(e => e.IdHistory);

                entity.ToTable("History");

                entity.Property(e => e.IdHistory).IsRequired().ValueGeneratedOnAdd();

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.HasOne(d => d.IdListOfCoinsNavigation)
                    .WithMany(p => p.Histories)
                    .HasForeignKey(d => d.IdListOfCoins)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_History_ListOfCoins");
            });

            modelBuilder.Entity<ListOfCoin>(entity =>
            {
                entity.HasKey(e => e.IdListOfCoins);

                entity.HasIndex(e => e.IdCoins, "IX_ListOfCoins_IdCoins");

                entity.HasIndex(e => e.IdUser, "IX_ListOfCoins_IdUser");

                entity.HasOne(d => d.IdCoinsNavigation)
                    .WithMany(p => p.ListOfCoins)
                    .HasForeignKey(d => d.IdCoins)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListOfCoins_Coins");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.ListOfCoins)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ListOfCoins_User");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.IdPayment);

                entity.ToTable("Payment");

                entity.HasIndex(e => e.IdListOfCoins, "IX_Payment_IdListOfCoins");

                entity.Property(e => e.PaymentAmount).HasColumnType("money");

                entity.Property(e => e.TimePayment).HasColumnType("datetime");

                entity.HasOne(d => d.IdListOfCoinsNavigation)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.IdListOfCoins)
                    .HasConstraintName("FK_Payment_ListOfCoins");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("User");

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.Login).HasMaxLength(16);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(16);

                entity.Property(e => e.Patronymic).HasMaxLength(50);

                entity.Property(e => e.Surname).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
