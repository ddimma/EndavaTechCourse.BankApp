using MediatR;
namespace EndavaTechCourse.BankApp.Application.Commands.AddCommision
{
	public class AddCommisionCommand : IRequest<CommandsStatus>
	{
		public string WalletType { get; set; }
		public decimal CommisionRate { get; set; }
	}
}

