using EndavaTechCourse.BankApp.Application.Commands.LoginUserCommand;
using EndavaTechCourse.BankApp.Application.Commands.RegisterUser;
using EndavaTechCourse.BankApp.Application.Queries.GetUserDetails;
using EndavaTechCourse.BankApp.Server.Common.JWTToken;
using EndavaTechCourse.BankApp.Server.Common;
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
        private readonly JwtService jwtService;

        public AccountController(IMediator mediator, JwtService jwtService)
		{
			ArgumentNullException.ThrowIfNull(mediator);
			this.mediator = mediator;
            this.jwtService = jwtService;
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var loginCommand = new LoginUserCommand()
            {
                Username = dto.Username,
                Password = dto.Password
            };

            var result = await mediator.Send(loginCommand);

            if (!result.IsSuccessful)
                return BadRequest(result.Error);

            var userDetailsQuery = new GetUserDetailsQuery()
            {
                Username = dto.Username
            };

            var userDetails = await mediator.Send(userDetailsQuery);

            string jwtToken = jwtService.CreateAuthToken(userDetails.Id, userDetails.Username, userDetails.Roles);

            Response.Cookies.Append(Constants.TokenCookieName, jwtToken, new CookieOptions()
            {
                HttpOnly = true,
                Expires = DateTimeOffset.MaxValue
            });

            return Ok(jwtToken);
        }


    }
}

