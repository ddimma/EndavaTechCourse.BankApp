using EndavaTechCourse.BankApp.Application.Commands.AddCurrency;
using EndavaTechCourse.BankApp.Application.Commands.DeteleCurrency;
using EndavaTechCourse.BankApp.Application.Commands.UpdateCurrency;
using EndavaTechCourse.BankApp.Application.Queries.GetCurrencies;
using EndavaTechCourse.BankApp.Application.Queries.GetCurrencyById;
using EndavaTechCourse.BankApp.Client.Pages;
using EndavaTechCourse.BankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
		[Route("add")]
        [Authorize(Roles = "Admin")]
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
		[Route("getById")]
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
		[Route("update")]
		public async Task<IActionResult> UpdateCurrency([FromBody] CurrencyDto currencyDto)
		{
			var currencyToUpdate = await mediator.Send(new UpdateCurrencyCommand
			{
				Id = currencyDto.Id,
				CurrencyCode = currencyDto.CurrencyCode,
				ChangeRate = currencyDto.ChangeRate,
				Name = currencyDto.Name
			});

			return currencyToUpdate.IsSuccessful ? Ok() : BadRequest(currencyToUpdate.Error);
		}

		[HttpPost("{currencyId}")]
		[Route("delete")]
        [Authorize(Roles = "Admin")]
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

