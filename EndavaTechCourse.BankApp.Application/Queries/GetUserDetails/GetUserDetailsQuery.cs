using MediatR;
using EndavaTechCourse.BankApp.Domain.Dtos;
namespace EndavaTechCourse.BankApp.Application.Queries.GetUserDetails
{
	public class GetUserDetailsQuery : IRequest<UserDetails>
	{
		public string Username { get; set; }
	}
}

