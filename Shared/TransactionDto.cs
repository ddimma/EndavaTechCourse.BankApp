namespace EndavaTechCourse.BankApp.Shared
{
	public class TransactionDto
	{
		public string Type { get; set; }
		public string Message { get; set; }
		public string SourceWalletId { get; set; }
		public string DestinationWalletId { get; set; }
        public string DestinationWalletCodeOrEmail { get; set; }
        public decimal TransactionAmount { get; set; }
	}
}