using EndavaTechCourse.BankApp.Domain.Models;
using MediatR;
namespace EndavaTechCourse.BankApp.Application.Queries.GetFavoriteWallets
{
	public class GetFavoriteWalletsQuery : IRequest<IEnumerable<Wallet>>
	{
        public string UserId { get; set; }
    }
}

