using EndavaTechCourse.BankApp.Application.Commands.AddCurrency;
using EndavaTechCourse.BankApp.Domain.Models;
using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EndavaTechCourse.BankApp.Application.Commands.DeteleCurrency
{
	public class DeleteCurrencyHandler : IRequestHandler<DeleteCurrencyCommand, CommandsStatus>
    {
        private readonly ApplicationDbContext context;

        public DeleteCurrencyHandler(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<CommandsStatus> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
        {
            Guid.TryParse(request.Id, out Guid currencyId);
            var currency = await context.Currencies
                .FirstOrDefaultAsync(x => x.Id == currencyId,
                cancellationToken);

            context.Currencies.Remove(currency);
            await context.SaveChangesAsync(cancellationToken);

            return new CommandsStatus();
        }
    }
}

