using EndavaTechCourse.BankApp.Shared;
using MediatR;
namespace EndavaTechCourse.BankApp.Application.Queries.GetWalletsForUser
{
	public class GetWalletsForUserQuery : IRequest<List<WalletDto>>
	{
		public string UserId { get; set; }
	}
}

