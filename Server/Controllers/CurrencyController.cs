using EndavaTechCourse.BankApp.Application.Commands.AddCurrency;
using EndavaTechCourse.BankApp.Application.Commands.DeteleCurrency;
using EndavaTechCourse.BankApp.Application.Queries.GetCurrencies;
using EndavaTechCourse.BankApp.Application.Queries.GetCurrencyById;
using EndavaTechCourse.BankApp.Client.Pages;
using EndavaTechCourse.BankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EndavaTechCourse.BankApp.Server.Controllers
{
	[Route("api/currencies")]
	[ApiController]
	public class CurrencyController : ControllerBase
	{
		public readonly IMediator mediator;

		public CurrencyController(IMediator mediator)
		{
			ArgumentNullException.ThrowIfNull(mediator);
			this.mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> AddCurrency([FromBody] CurrencyDto dto)
		{
			var command = new AddCurrencyCommand()
			{
				Name = dto.Name,
				CurrencyCode = dto.CurrencyCode,
				ChangeRate = dto.ChangeRate
			};

			var result = await mediator.Send(command);

			return result.IsSuccessful ? Ok() : BadRequest(result.Error);
		}

		[HttpGet]
		public async Task<List<CurrencyDto>> GetCurrencies()
		{
			var currencies = await mediator.Send(new GetCurrenciesQuery());
			var currenciesList = new List<CurrencyDto>();
			foreach (var currency in currencies)
			{
				var newCurrency = new CurrencyDto
				{
					Id = currency.Id.ToString(),
					Name = currency.Name,
					ChangeRate = currency.ChangeRate,
					CurrencyCode = currency.CurrencyCode

				};
				currenciesList.Add(newCurrency);
			}
			return currenciesList;
		}

		[HttpGet("{currencyId}")]
		public async Task<CurrencyDto> GetCurrencyById(string currencyId)
		{
			var currency = await mediator.Send(new GetCurrencyByIdQuery
			{
				Id = currencyId
			});

			if (currency == null)
			{
				return new CurrencyDto();
			}

			var currencyDto = new CurrencyDto
			{
				Id = currency.Id.ToString(),
				Name = currency.Name,
				ChangeRate = currency.ChangeRate,
				CurrencyCode = currency.CurrencyCode

			};

			return currencyDto;
		}

		[HttpPost("{currencyId}")]
		public async Task<IActionResult> DeleteCurrency(string currencyId)
		{
			var currencyToDelete = await mediator.Send(new DeleteCurrencyCommand
			{
				Id = currencyId
			});
            return currencyToDelete.IsSuccessful ? Ok() : BadRequest(currencyToDelete.Error);
        }
	}
}

