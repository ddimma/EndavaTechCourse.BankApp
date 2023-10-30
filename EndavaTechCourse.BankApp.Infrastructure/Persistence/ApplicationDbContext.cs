using Microsoft.EntityFrameworkCore;
using EndavaTechCourse.BankApp.Domain.Models;

namespace EndavaTechCourse.BankApp.Infrastructure.Persistence
{
	public class ApplicationDbContext : DbContext
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>().HasKey(x => x.Id);
            modelBuilder.Entity<Currency>().HasKey(x => x.Id);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Wallet> Wallets { get; set; }
		public DbSet<Currency> Currencies { get; set; }

        
    }

}

