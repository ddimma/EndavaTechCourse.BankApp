using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using MediatR;

namespace EndavaTechCourse.BankApp.Application.Commands.UpdateCurrency
{
    public class UpdateCurrencyHandler : IRequestHandler<UpdateCurrencyCommand, CommandsStatus>
    {
        private readonly ApplicationDbContext context;

        public UpdateCurrencyHandler(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<CommandsStatus> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
        {
            var existingCurrency = await context.Currencies.FindAsync(request.Id);

            if (existingCurrency == null)
            {
                return CommandsStatus.Failed("Valuta nu a fost găsită.");
            }

            existingCurrency.Name = request.Name;
            existingCurrency.CurrencyCode = request.CurrencyCode;
            existingCurrency.ChangeRate = request.ChangeRate;

            await context.SaveChangesAsync(cancellationToken);

            return new CommandsStatus();
        }
    }
}

