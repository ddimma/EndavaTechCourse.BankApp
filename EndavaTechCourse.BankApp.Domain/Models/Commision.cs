using EndavaTechCourse.BankApp.Domain.Common;

namespace EndavaTechCourse.BankApp.Domain.Models
{
	public class Commision : BaseEntity
	{
		public string WalletType { get; set; }
		public decimal CommisionRate { get; set; }
	}
}

