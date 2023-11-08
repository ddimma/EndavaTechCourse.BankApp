using System;
using EndavaTechCourse.BankApp.Application.Queries.GetCurrencies;
using EndavaTechCourse.BankApp.Domain.Models;
using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EndavaTechCourse.BankApp.Application.Queries.GetCurrencyById
{
    public class GetCurrencyByIdHandler : IRequestHandler<GetCurrencyByIdQuery, Currency>
    {
        private readonly ApplicationDbContext context;

        public GetCurrencyByIdHandler(ApplicationDbContext context, IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<Currency> Handle(GetCurrencyByIdQuery request, CancellationToken cancellationToken)
        {
            Guid.TryParse(request.Id, out Guid currencyId);
            var currency = await context.Currencies
                .Where(c => c.Id == currencyId)
                .FirstOrDefaultAsync(cancellationToken);

            return currency;
        }
    }
}

