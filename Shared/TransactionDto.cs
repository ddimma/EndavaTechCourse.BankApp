using System;
namespace EndavaTechCourse.BankApp.Shared
{
	public class TransactionDto
	{
		public string Message { get; set; }
		public string SourceWalletId { get; set; }
		public string DestinationWalletId { get; set; }
		public decimal TransactionAmount { get; set; }
	}
}

