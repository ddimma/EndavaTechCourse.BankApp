using EndavaTechCourse.BankApp.Domain.Models;
using MediatR;
namespace EndavaTechCourse.BankApp.Application.Queries.GetTransactions
{
	public class GetTransactionsQuery : IRequest<List<Transaction>>
	{
		
	}
}

