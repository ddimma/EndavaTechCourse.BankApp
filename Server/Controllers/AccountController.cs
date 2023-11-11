using EndavaTechCourse.BankApp.Application.Commands.RegisterUser;
using EndavaTechCourse.BankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EndavaTechCourse.BankApp.Server.Controllers
{
	[Route("api/account")]
	[ApiController]
	public class AccountController : ControllerBase
	{
        private readonly IMediator mediator;

        public AccountController(IMediator mediator)
		{
			ArgumentNullException.ThrowIfNull(mediator);
			this.mediator = mediator;
		}

		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
		{
			var registerUserCommand = new RegisterUserCommand()
			{
				Username = registerDto.Username,
				FirstName = registerDto.FirstName,
				LastName = registerDto.LastName,
				Password = registerDto.Password,
				Email = registerDto.Email
			};

			var result = await mediator.Send(registerUserCommand);

			return result.IsSuccessful ? Ok() : BadRequest(new { result.Error });
		}
	}
}

