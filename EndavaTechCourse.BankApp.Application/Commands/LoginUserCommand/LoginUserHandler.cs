﻿using EndavaTechCourse.BankApp.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EndavaTechCourse.BankApp.Application.Commands.LoginUserCommand
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, CommandsStatus>
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public LoginUserHandler(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            ArgumentNullException.ThrowIfNull(userManager);
            ArgumentNullException.ThrowIfNull(signInManager);

            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<CommandsStatus> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.Username);

            if (user is null)
                return CommandsStatus.Failed("Nu exista un asemenea utilizator");

            var passwordStatus = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!passwordStatus.Succeeded)
                return CommandsStatus.Failed("Parola introdusa este gresita");

            return new();
        }
    }
}

