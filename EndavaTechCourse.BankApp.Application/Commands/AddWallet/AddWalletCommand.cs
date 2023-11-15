using MediatR;
namespace EndavaTechCourse.BankApp.Application.Commands.AddWallet
{
	public class AddWalletCommand : IRequest<CommandsStatus>
	{
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Id { get; set; }
        public string UserId { get; set; }
    }
}

