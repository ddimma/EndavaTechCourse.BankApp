using MediatR;
namespace EndavaTechCourse.BankApp.Application.Commands.Transaction
{
	public class TransactionCommand : IRequest<CommandsStatus>
	{
        public string Message { get; set; }
        public string SourceWalletId { get; set; }
        public string DestinationWalletId { get; set; }
        public decimal TransactionAmount { get; set; }
    }
}

