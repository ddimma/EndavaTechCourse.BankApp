using EndavaTechCourse.BankApp.Domain.Models;
using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EndavaTechCourse.BankApp.Application.Queries.GetCurrencies
{
    public class GetCurrenciesHandler : IRequestHandler<GetCurrenciesQuery, List<Currency>>
    {
        private readonly ApplicationDbContext context;


        public GetCurrenciesHandler(ApplicationDbContext context, IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<List<Currency>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
        {
            var currencies = await context.Currencies.AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);

            return currencies;
        }
    }
}

