﻿using Microsoft.EntityFrameworkCore;
using EndavaTechCourse.BankApp.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EndavaTechCourse.BankApp.Infrastructure.Configurations;

namespace EndavaTechCourse.BankApp.Infrastructure.Persistence
{
	public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

        public DbSet<Wallet> Wallets { get; set; }
		public DbSet<Currency> Currencies { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>().HasKey(x => x.Id);
            modelBuilder.Entity<Transaction>().HasKey(x => x.Id);
            modelBuilder.Entity<Currency>().HasKey(x => x.Id);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.Wallets)
                .WithOne(e => e.Currency)
                .HasForeignKey(e => e.CurrencyId)
                .IsRequired();

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.SourceWallet)
                .WithMany()
                .HasForeignKey(t => t.SourceWalletId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.DestinationWallet)
                .WithMany()
                .HasForeignKey(t => t.DestinationWalletId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfigurations());
        }
    }

}

