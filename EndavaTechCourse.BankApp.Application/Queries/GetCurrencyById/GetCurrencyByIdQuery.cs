using System;
using EndavaTechCourse.BankApp.Domain.Models;
using MediatR;

namespace EndavaTechCourse.BankApp.Application.Queries.GetCurrencyById
{
	public class GetCurrencyByIdQuery : IRequest<Currency>
    {
		public string Id { get; set; }
	}
}

