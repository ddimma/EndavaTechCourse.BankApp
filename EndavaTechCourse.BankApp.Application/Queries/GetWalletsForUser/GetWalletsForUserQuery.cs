using EndavaTechCourse.BankApp.Domain.Models;
using MediatR;
namespace EndavaTechCourse.BankApp.Application.Queries.GetWalletsForUser
{
	public class GetWalletsForUserQuery : IRequest<List<Wallet>>
	{
		public string UserId { get; set; }
	}
}

