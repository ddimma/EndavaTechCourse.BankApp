using System;
using MediatR;

namespace EndavaTechCourse.BankApp.Application.Commands.UpdateWallet
{
	public class UpdateWalletCommand : IRequest<CommandsStatus>
	{
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Id { get; set; }
    }
}

