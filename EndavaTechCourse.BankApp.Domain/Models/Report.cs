using EndavaTechCourse.BankApp.Domain.Common;

namespace EndavaTechCourse.BankApp.Domain.Models
{
	public class Report : BaseEntity
	{
		public DateTime Period { get; set; }
		public int TransactionsNumber { get; set; }
		public decimal Income { get; set; }
		public decimal Expenses { get; set; }
		public decimal Balance { get; set; }

	}
}

