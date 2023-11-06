using System;
using EndavaTechCourse.BankApp.Domain.Models;
using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EndavaTechCourse.BankApp.Application.Queries.GetWallets
{
    public class GetWalletsHandler : IRequestHandler<GetWalletsQuery, List<Wallet>>
    {
        private readonly ApplicationDbContext context;
        

        public GetWalletsHandler(ApplicationDbContext context, IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<List<Wallet>> Handle(GetWalletsQuery request, CancellationToken cancellationToken)
        {
            var wallets = await context.Wallets
                .Include(x => x.Currency).AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);

            return wallets;
        }
    }
}

