using EndavaTechCourse.BankApp.Domain.Models;
using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
namespace EndavaTechCourse.BankApp.Application.Commands.UpdateWallet
{
    public class UpdateWalletHandler : IRequestHandler<UpdateWalletCommand, CommandsStatus>
    {
        private readonly ApplicationDbContext context;

        public UpdateWalletHandler(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<CommandsStatus> Handle(UpdateWalletCommand request, CancellationToken cancellationToken)
        {
            var existingWallet = await context.Wallets.FindAsync(request.Id);

            if (existingWallet == null)
            {
                return CommandsStatus.Failed("Wallet nu a fost găsit.");
            }

            existingWallet.Type = request.Type;
            existingWallet.Currency = new Currency
            {
                CurrencyCode = request.Currency
            };
            existingWallet.Amount = request.Amount;

            await context.SaveChangesAsync(cancellationToken);

            return new CommandsStatus();
        }
    }
}

