
using MediatR;
namespace EndavaTechCourse.BankApp.Application.Commands.LoginUserCommand
{
	public class LoginUserCommand : IRequest<CommandsStatus>
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}

