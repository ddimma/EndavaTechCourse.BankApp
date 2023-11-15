using EndavaTechCourse.BankApp.Domain.Common;

namespace EndavaTechCourse.BankApp.Domain.Models
{
	public class Transaction : BaseEntity
	{
		public string Message { get; set; }
		public decimal TransactionAmount { get; set; }
		public Guid SourceWalletId { get; set; }
		public Wallet SourceWallet { get; set; }
		public Guid DestinationWalletId { get; set; }
		public Wallet DestinationWallet { get; set; }
    }
}

