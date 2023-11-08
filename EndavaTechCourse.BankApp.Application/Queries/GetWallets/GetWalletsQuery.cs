using System;
using EndavaTechCourse.BankApp.Domain.Models;
using MediatR;

namespace EndavaTechCourse.BankApp.Application.Queries.GetWallets
{
	public class GetWalletsQuery : IRequest<List<Wallet>>
	{
		
	}
}

