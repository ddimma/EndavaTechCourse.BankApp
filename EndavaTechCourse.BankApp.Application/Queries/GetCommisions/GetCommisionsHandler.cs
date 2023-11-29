using MediatR;
using EndavaTechCourse.BankApp.Domain.Models;
using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EndavaTechCourse.BankApp.Application.Queries.GetCommisions
{
	public class GetCommisionsHandler : IRequestHandler<GetCommisionsQuery, IEnumerable<Commision>>
	{
        private readonly ApplicationDbContext context;

        public GetCommisionsHandler(ApplicationDbContext context)
		{
            ArgumentNullException.ThrowIfNull(context);
            this.context = context;
        }

        public async Task<IEnumerable<Commision>> Handle(GetCommisionsQuery request, CancellationToken cancellationToken)
        {
            var commisions = await context.Commisions.AsNoTracking()
                .ToListAsync(cancellationToken: cancellationToken);

            return commisions;
        }
    }
}

