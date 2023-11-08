using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using EndavaTechCourse.BankApp.Shared;
using EndavaTechCourse.BankApp.Tests.Common;
using EndavaTechCourse.BankApp.Server.Controllers;
using EndavaTechCourse.BankApp.Domain.Models;
using FluentAssertions;
using AutoFixture.Idioms;
using MediatR;
using NSubstitute;
using EndavaTechCourse.BankApp.Application.Commands.AddWallet;
using EndavaTechCourse.BankApp.Application.Commands;
using EndavaTechCourse.BankApp.Application.Commands.AddCurrency;
using EndavaTechCourse.BankApp.Application.Queries.GetCurrencies;
using Microsoft.AspNetCore.Mvc;

namespace EndavaTechCourse.BankApp.Tests.ControllersTests
{
	public class CurrencyControllerTests
	{
        [Test, ApplicationData]
        public async Task ShouldSendAddCurrencyCommand(
            [Frozen] IMediator mediator,
            [Greedy] CurrencyController controller,
            CurrencyDto currencyDto)
        {
            mediator.Send(Arg.Any<AddCurrencyCommand>(), default).Returns(new CommandsStatus());

            await controller.AddCurrency(currencyDto);

            await mediator.Received(1).Send(Arg.Any<AddCurrencyCommand>(), default);
        }

        [Test, ApplicationData]
        public async Task ShouldSendGetCurrenciesQuery(
            [Frozen] IMediator mediator,
            [Greedy] CurrencyController controller)
        {
            mediator.Send(Arg.Any<GetCurrenciesQuery>(), default)
            .Returns(new List<Currency>());

            // Act
            await controller.GetCurrencies();

            // Assert
            await mediator.Received(1).Send(Arg.Any<GetCurrenciesQuery>(), default);
        }
    }
}

