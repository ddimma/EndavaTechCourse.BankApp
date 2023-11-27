using EndavaTechCourse.BankApp.Domain.Dtos;
using EndavaTechCourse.BankApp.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EndavaTechCourse.BankApp.Application.Queries.GetUserDetails
{
    public class GetUserDetailsHandler : IRequestHandler<GetUserDetailsQuery, UserDetails>
    {
        private readonly UserManager<User> userManager;

        public GetUserDetailsHandler(UserManager<User> userManager)
        {
            ArgumentNullException.ThrowIfNull(userManager);

            this.userManager = userManager;
        }

        public async Task<UserDetails> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.Username);

            if (user is null)
                return new();

            var roleStrings = await userManager.GetRolesAsync(user);

            return new()
            {
                Id = user.Id.ToString(),
                Roles = roleStrings.ToArray(),
                Username = request.Username,
            };
        }
    }
}

