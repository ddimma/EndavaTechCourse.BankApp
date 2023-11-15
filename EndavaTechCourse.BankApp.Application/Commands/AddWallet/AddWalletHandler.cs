using EndavaTechCourse.BankApp.Domain.Models;
using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using MediatR;

namespace EndavaTechCourse.BankApp.Application.Commands.AddWallet
{
	public class AddWalletHandler : IRequestHandler<AddWalletCommand, CommandsStatus>
    {
        private readonly ApplicationDbContext context;

        public AddWalletHandler(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<CommandsStatus> Handle(AddWalletCommand request, CancellationToken cancellationToken)
        {
            Guid.TryParse(request.UserId, out Guid userId);
            var wallet = new Wallet()
            {
                Type = request.Type,
                Amount = request.Amount,
                Currency = new Currency()
                {
                    CurrencyCode = request.Currency
                },
                UserId = userId
            };

            await context.Wallets.AddAsync(wallet, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return new CommandsStatus();
        }
    }
}

