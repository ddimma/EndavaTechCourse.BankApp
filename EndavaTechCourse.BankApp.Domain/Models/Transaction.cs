using EndavaTechCourse.BankApp.Domain.Common;

namespace EndavaTechCourse.BankApp.Domain.Models
{
	public class Transaction : BaseEntity
	{
		public string Type { get; set; }
		public decimal Amount { get; set; }
		public Guid SenderId { get; set; } // ??
		public Guid ReceiverId { get; set; } // ??
        public Currency TransactionCurrency { get; set; } // ??
    }
}

