using MediatR;

namespace EndavaTechCourse.BankApp.Application.Commands.DeteleCurrency
{
	public class DeleteCurrencyCommand : IRequest<CommandsStatus>
	{
		public string Id { get; set; }
	}
}

