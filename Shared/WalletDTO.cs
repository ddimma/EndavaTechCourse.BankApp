using System;
namespace EndavaTechCourse.BankApp.Shared
{
	public class CreateWalletDTO
	{
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public CurrencyDTO CurrencyDTO { get; set; }
    }

    public class CurrencyDTO
    {
        public decimal ChangeRate { get; set; }
        public string Name { get; set; }
        public string CurrencyCode { get; set; }
    }

    public class GetWalletDTO
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}

