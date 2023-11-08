using MediatR;

namespace EndavaTechCourse.BankApp.Application.Commands.DeleteWallet
{
	public class DeleteWalletCommand : IRequest<CommandsStatus>
	{
		public string Id { get; set; }
	}
}

