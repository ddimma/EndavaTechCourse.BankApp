using EndavaTechCourse.BankApp.Application.Commands.DeteleCurrency;
using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EndavaTechCourse.BankApp.Application.Commands.DeleteWallet
{
	public class DeleteWalletHandler : IRequestHandler<DeleteWalletCommand, CommandsStatus>
	{
        private readonly ApplicationDbContext context;
        public DeleteWalletHandler(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<CommandsStatus> Handle(DeleteWalletCommand request, CancellationToken cancellationToken)
        {
            Guid.TryParse(request.Id, out Guid walletId);
            var currency = await context.Wallets
                .FirstOrDefaultAsync(x => x.Id == walletId,
                cancellationToken);

            context.Wallets.Remove(currency);
            await context.SaveChangesAsync(cancellationToken);

            return new CommandsStatus();
        }
    }
}

