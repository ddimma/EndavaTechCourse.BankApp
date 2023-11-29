using EndavaTechCourse.BankApp.Domain.Models;
using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EndavaTechCourse.BankApp.Application.Commands.DeleteCommision
{
	public class DeleteCommisionHandler : IRequestHandler<DeleteCommisionCommand, CommandsStatus>
	{
		private readonly ApplicationDbContext context;
		public DeleteCommisionHandler(ApplicationDbContext context)
		{
			ArgumentNullException.ThrowIfNull(context);
			this.context = context;
		}

        public async Task<CommandsStatus> Handle(DeleteCommisionCommand request, CancellationToken cancellationToken)
        {
			Guid.TryParse(request.Id, out Guid commisionId);
			var commision = await context.Commisions
				.FirstOrDefaultAsync(c => c.Id == commisionId, cancellationToken);

            context.Commisions.Remove(commision);
            await context.SaveChangesAsync(cancellationToken);

            return new CommandsStatus();
        }
    }
}

