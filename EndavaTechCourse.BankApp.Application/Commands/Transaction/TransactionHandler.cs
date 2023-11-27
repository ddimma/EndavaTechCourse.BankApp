using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using EndavaTechCourse.BankApp.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using EndavaTechCourse.BankApp.Domain.Models;

namespace EndavaTechCourse.BankApp.Application.Commands.Transaction
{
    public class TransactionHandler : IRequestHandler<TransactionCommand, CommandsStatus>
    {
        private readonly ApplicationDbContext context;
        private readonly CurrencyConverter currencyConverter;

        public TransactionHandler(ApplicationDbContext context, CurrencyConverter currencyConverter)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
            this.currencyConverter = currencyConverter;
        }

        public async Task<CommandsStatus> Handle(TransactionCommand request, CancellationToken cancellationToken)
        {
            var destinationWallet = new Wallet();
            Guid.TryParse(request.SourceWalletId, out var source);
            Guid.TryParse(request.DestinationWalletId, out var destination);
            var sourceWallet = await context.Wallets
                .Include(w => w.Currency)
                .Include(w => w.User)
                .FirstOrDefaultAsync(w => w.Id == source);
            if (request.DestinationWalletCodeOrEmail == null)
            {
                destinationWallet = await context.Wallets
                .Include(w => w.Currency)
                .Include(w => w.User)
                .FirstOrDefaultAsync(w => w.Id == destination);
            }
            else
            {
                destinationWallet = await context.Wallets
                .Include(w => w.Currency)
                .Include(w => w.User)
                .OrderByDescending(w => w.IsMainWallet)
                .FirstOrDefaultAsync(w => w.WalletCode == request.DestinationWalletCodeOrEmail || w.User.Email == request.DestinationWalletCodeOrEmail);
                
            }
            

            if (sourceWallet == null || destinationWallet == null) 
            {
                return CommandsStatus.Failed("0 Wallet sursă sau destinație nu a fost găsit.");
            }
            if (request.TransactionAmount < 0)
            {
                return CommandsStatus.Failed("0 Valoarea transferului nevalidă.");
            }
            if (sourceWallet.Amount < request.TransactionAmount)
            {
                return CommandsStatus.Failed("0 Resurse insuficiente.");
            }

            decimal convertedAmount = currencyConverter.ConvertCurrency(request.TransactionAmount, sourceWallet.Currency.CurrencyCode, destinationWallet.Currency.CurrencyCode);

            sourceWallet.Amount -= request.TransactionAmount;

            destinationWallet.Amount += convertedAmount;

            var newTransaction = new Domain.Models.Transaction()
            {
                Message = request.Message,
                SourceWallet = sourceWallet,
                DestinationWallet = destinationWallet,
                SourceWalletId = sourceWallet.Id,
                DestinationWalletId = destinationWallet.Id,
                TransactionAmount = convertedAmount
            };

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    await context.Transactions.AddAsync(newTransaction, cancellationToken);
                    await context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return new CommandsStatus();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return CommandsStatus.Failed("0 A apătut o eroare.");
                }
            }
        }
    }
}

