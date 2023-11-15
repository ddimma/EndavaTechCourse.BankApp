using EndavaTechCourse.BankApp.Application.Queries.GetCurrencies;
using EndavaTechCourse.BankApp.Domain.Models;
using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EndavaTechCourse.BankApp.Application.Queries.GetTransactions
{
	public class GetTransactionsHandler : IRequestHandler<GetTransactionsQuery, List<Transaction>>
	{
        private readonly ApplicationDbContext context;


        public GetTransactionsHandler(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<List<Transaction>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactions = await context.Transactions.AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);

            return transactions;
        }
    }
}

