using EndavaTechCourse.BankApp.Domain.Models;
using MediatR;
namespace EndavaTechCourse.BankApp.Application.Queries.GetCommisions
{
	public class GetCommisionsQuery : IRequest<IEnumerable<Commision>>
	{
	}
}

