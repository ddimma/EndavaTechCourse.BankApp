using Microsoft.EntityFrameworkCore;
using EndavaTechCourse.BankApp.Domain.Models;

namespace EndavaTechCourse.BankApp.Infrastructure.Persistence
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public DbSet<Wallet> Wallets { get; set; }
		public DbSet<Currency> Currencies { get; set; }
	}

}

