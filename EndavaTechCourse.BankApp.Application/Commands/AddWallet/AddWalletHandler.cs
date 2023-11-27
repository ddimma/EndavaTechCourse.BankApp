using EndavaTechCourse.BankApp.Domain.Models;
using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using EndavaTechCourse.BankApp.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EndavaTechCourse.BankApp.Application.Commands.AddWallet
{
	public class AddWalletHandler : IRequestHandler<AddWalletCommand, CommandsStatus>
    {
        private readonly ApplicationDbContext context;
        private readonly WalletCodeGenerator walletCodeGenerator;

        public AddWalletHandler(ApplicationDbContext context, WalletCodeGenerator walletCodeGenerator)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(walletCodeGenerator);
            this.context = context;
            this.walletCodeGenerator = walletCodeGenerator;
        }

        public async Task<CommandsStatus> Handle(AddWalletCommand request, CancellationToken cancellationToken)
        {
            Guid.TryParse(request.UserId, out Guid userId);
            Guid.TryParse(request.Id, out Guid walletId);
            bool isFirstUserWallet = !context.Wallets.Any(w => w.UserId == userId && w.Id != walletId);
            var existingCurrency = context.Currencies.SingleOrDefault(c => c.CurrencyCode == request.Currency);
            var wallet = new Wallet()
            {
                Type = request.Type,
                Amount = request.Amount,
                Currency = existingCurrency,
                UserId = userId,
                WalletCode = walletCodeGenerator.GenerateWalletCode(),
            };

            if (isFirstUserWallet)
            {
                wallet.IsMainWallet = true;
            }

            await context.Wallets.AddAsync(wallet, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return new CommandsStatus();
        }
    }
}

