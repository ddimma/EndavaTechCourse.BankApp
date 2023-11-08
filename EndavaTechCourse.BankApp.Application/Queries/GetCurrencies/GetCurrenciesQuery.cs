using System;
using EndavaTechCourse.BankApp.Domain.Models;
using MediatR;

namespace EndavaTechCourse.BankApp.Application.Queries.GetCurrencies
{
	public class GetCurrenciesQuery : IRequest<List<Currency>>
    {
    
	}
}

