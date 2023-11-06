using EndavaTechCourse.BankApp.Domain.Models;
using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EndavaTechCourse.BankApp.Application.Commands.AddCurrency
{
    public class AddCurrencyHandler : IRequestHandler<AddCurrencyCommand, CommandsStatus>
    {
        private readonly ApplicationDbContext context;

        public AddCurrencyHandler(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<CommandsStatus> Handle(AddCurrencyCommand request, CancellationToken cancellationToken)
        {
            if (await context.Currencies.AnyAsync(x => x.Name == request.Name, default))
                return CommandsStatus.Failed("0 Valuta cu aceasta denumire deja exista.");

            if (await context.Currencies.AnyAsync(x => x.CurrencyCode == request.CurrencyCode, default))
                return CommandsStatus.Failed("0 Valuta cu acest cod deja exista.");

            var currency = new Currency()
            {
                Name = request.Name,
                CurrencyCode = request.CurrencyCode,
                ChangeRate = request.ChangeRate
            };

            await context.Currencies.AddAsync(currency, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return new CommandsStatus();
        }
    }
}

