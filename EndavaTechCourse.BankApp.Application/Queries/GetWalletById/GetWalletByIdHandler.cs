using EndavaTechCourse.BankApp.Domain.Models;
using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EndavaTechCourse.BankApp.Application.Queries.GetWalletById
{
	public class GetWalletByIdHandler : IRequestHandler<GetWalletByIdQuery, Wallet>
	{
        private readonly ApplicationDbContext context;

        public GetWalletByIdHandler(ApplicationDbContext context, IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<Wallet> Handle(GetWalletByIdQuery request, CancellationToken cancellationToken)
        {
            Guid.TryParse(request.Id, out Guid walletId);
            var wallet = await context.Wallets
                .Include(w => w.Currency)
                .Where(c => c.Id == walletId)
                .FirstOrDefaultAsync(cancellationToken);

            return wallet;
        }
    }
}

