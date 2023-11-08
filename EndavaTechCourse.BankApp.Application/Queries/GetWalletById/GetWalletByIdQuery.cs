using EndavaTechCourse.BankApp.Domain.Models;
using MediatR;
namespace EndavaTechCourse.BankApp.Application.Queries.GetWalletById
{
	public class GetWalletByIdQuery : IRequest<Wallet>
	{
		public string Id { get; set; }
	}
}

