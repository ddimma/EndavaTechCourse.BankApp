using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
namespace EndavaTechCourse.BankApp.Application.Commands.UpdateCommision
{
	public class UpdateCommisionHandler : IRequestHandler<UpdateCommisionCommand, CommandsStatus>
	{
        private readonly ApplicationDbContext context;
		public UpdateCommisionHandler(ApplicationDbContext context)
		{
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<CommandsStatus> Handle(UpdateCommisionCommand request, CancellationToken cancellationToken)
        {
            Guid.TryParse(request.CommisionId, out Guid commisionId);
            var existingCommision = await context.Commisions.FindAsync(commisionId);

            if (existingCommision == null)
            {
                return CommandsStatus.Failed("Comision nu a fost găsită.");
            }
            existingCommision.CommisionRate = request.CommisionRate;
            existingCommision.WalletType = request.WalletType;

            await context.SaveChangesAsync(cancellationToken);

            return new CommandsStatus();
        }
    }
}

