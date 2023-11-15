using EndavaTechCourse.BankApp.Domain.Common;
namespace EndavaTechCourse.BankApp.Domain.Models
{
	public class Wallet : BaseEntity
	{
		public decimal Amount { get; set; }
		public Guid CurrencyId { get; set; }
		public Currency Currency { get; set; }
		public string Type { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}

