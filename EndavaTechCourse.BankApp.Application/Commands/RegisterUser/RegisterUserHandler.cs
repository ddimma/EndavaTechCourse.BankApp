using EndavaTechCourse.BankApp.Domain.Enums;
using EndavaTechCourse.BankApp.Domain.Models;
using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EndavaTechCourse.BankApp.Application.Commands.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, CommandsStatus>
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;

        public RegisterUserHandler(ApplicationDbContext context, UserManager<User> userManager)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(userManager);

            this.context = context;
            this.userManager = userManager;
        }

        public async Task<CommandsStatus> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var notFirstUser = await context.Users.AnyAsync(cancellationToken);
            var userExists = await context.Users.AnyAsync(user => user.Email == request.Email, cancellationToken);

            if (userExists)
                return CommandsStatus.Failed("Utilizatorul deja exista");

            var user = new User()
            {
                Id = Guid.NewGuid(),
                UserName = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };

            var createResult = await userManager.CreateAsync(user, request.Password);

            IdentityResult roleResult;

            if (notFirstUser)
                roleResult = await userManager.AddToRoleAsync(user, UserRole.User.ToString());
            else
                roleResult = await userManager.AddToRoleAsync(user, UserRole.Admin.ToString());

            if (!roleResult.Succeeded || !createResult.Succeeded)
                return CommandsStatus.Failed("Utilizatorul nu a putut fi inregistrat");

            return new CommandsStatus();
        }
    }
}

