using EndavaTechCourse.BankApp.Domain.Models;
using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EndavaTechCourse.BankApp.Application.Queries.GetFavoriteWallets
{
	public class GetFavoriteWalletsHandler : IRequestHandler<GetFavoriteWalletsQuery, IEnumerable<Wallet>>
	{
        private readonly ApplicationDbContext context;

        public GetFavoriteWalletsHandler(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<IEnumerable<Wallet>> Handle(GetFavoriteWalletsQuery request, CancellationToken cancellationToken)
        {
            Guid.TryParse(request.UserId, out Guid userId);
            var wallets = await context.Wallets
                .Where(w => w.UserId == userId && w.IsFavorite == true)
                .Include(x => x.Currency).AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);

            return wallets;
        }
    }
}

