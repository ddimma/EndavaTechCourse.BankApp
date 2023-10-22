using EndavaTechCourse.BankApp.Domain.Common;
namespace EndavaTechCourse.BankApp.Domain.Models
{
	public class Wallet : BaseEntity
	{
		public decimal Amount { get; set; }
		public Currency Currency { get; set; }
		public string Type { get; set; }
		
	}
}

