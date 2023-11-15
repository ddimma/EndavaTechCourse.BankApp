﻿using Microsoft.AspNetCore.Identity;

namespace EndavaTechCourse.BankApp.Domain.Models
{
	public class User : IdentityUser<Guid>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
        public ICollection<Wallet> Wallets { get; set; }
    }
}