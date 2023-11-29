using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
namespace EndavaTechCourse.BankApp.Application.Commands.ToggleFavoriteWallet
{
	public class ToggleFavoriteWalletHandler : IRequestHandler <ToggleFavoriteWalletCommand, CommandsStatus>
	{
        private readonly ApplicationDbContext context;
        public ToggleFavoriteWalletHandler(ApplicationDbContext context)
		{
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<CommandsStatus> Handle(ToggleFavoriteWalletCommand request, CancellationToken cancellationToken)
        {
            Guid.TryParse(request.Id, out Guid id);
            var existingWallet = await context.Wallets.FindAsync(id);

            if (existingWallet == null)
            {
                return CommandsStatus.Failed("Wallet nu a fost găsit.");
            }

            existingWallet.IsFavorite = !existingWallet.IsFavorite;

            await context.SaveChangesAsync(cancellationToken);

            return new CommandsStatus();
        }
    }
}

