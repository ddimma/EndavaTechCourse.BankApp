﻿using EndavaTechCourse.BankApp.Domain.Common;
namespace EndavaTechCourse.BankApp.Domain.Models
{
	public class Currency : BaseEntity
	{
		public decimal ChangeRate { get; set; }
		public string Name { get; set; }
		public string CurrencyCode { get; set; }
		public List<Wallet> Wallets { get; set; } = new List<Wallet>();
	}
}

