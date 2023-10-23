using EndavaTechCourse.BankApp.Domain.Common;

namespace EndavaTechCourse.BankApp.Domain.Models
{
	public class CashByCode : BaseEntity
	{
		public string Code { get; set; }
		public decimal Amount { get; set; }
		public bool IsValid { get; set; }
	}
}

