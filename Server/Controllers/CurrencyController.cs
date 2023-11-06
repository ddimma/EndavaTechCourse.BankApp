using System;
using EndavaTechCourse.BankApp.Application.Commands.AddCurrency;
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
	}
}

