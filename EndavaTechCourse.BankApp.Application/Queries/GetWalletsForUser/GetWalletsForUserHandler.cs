using EndavaTechCourse.BankApp.Domain.Models;
using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EndavaTechCourse.BankApp.Application.Queries.GetWalletsForUser
{
    public class GetWalletsForUserHandler : IRequestHandler<GetWalletsForUserQuery, List<Wallet>>
    {
        private readonly ApplicationDbContext context;


        public GetWalletsForUserHandler(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<List<Wallet>> Handle(GetWalletsForUserQuery request, CancellationToken cancellationToken)
        {
            Guid.TryParse(request.UserId, out Guid userId);
            var wallets = await context.Wallets
                .Where(w => w.UserId == userId)
                .Include(x => x.Currency).AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);

            return wallets;

        }
    }
}

