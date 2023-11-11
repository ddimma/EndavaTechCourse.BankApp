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
using Moq;
using Microsoft.VisualBasic;
using NSubstitute.Core;
using NUnit.Framework.Internal;
using EndavaTechCourse.BankApp.Application.Queries.GetCurrencyById;
using EndavaTechCourse.BankApp.Application.Commands.DeteleCurrency;

namespace EndavaTechCourse.BankApp.Tests.ControllersTests
{
    [TestFixture]
	public class CurrencyControllerTests
	{
        private readonly Mock<IMediator> _mediator;
        private readonly CurrencyController _controller;
        public CurrencyControllerTests()
        {
            _mediator = new Mock<IMediator>();
            _controller = new CurrencyController(_mediator.Object);

        }
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
            // Arrange
            var currencies = new List<Currency>
            {
                new Currency { Name = "USD", ChangeRate = 1, CurrencyCode = "USD" },
                new Currency { Name = "EUR", ChangeRate = 3, CurrencyCode = "EUR" }
        
            };

            mediator.Send(Arg.Any<GetCurrenciesQuery>(), default).Returns(currencies);

            // Act
            var result = await controller.GetCurrencies();

            // Assert
            await mediator.Received(1).Send(Arg.Any<GetCurrenciesQuery>(), default);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<CurrencyDto>>(result);
            Assert.AreEqual(currencies.Count, result.Count);
        }

        [Test, ApplicationData]
        public async Task ShouldSendGetCurrencyByIdQueruy(
            [Frozen] IMediator mediator,
            [Greedy] CurrencyController controller)
        {
            var currency = new Currency { Name = "USD", ChangeRate = 1, CurrencyCode = "USD" };

            mediator.Send(Arg.Any<GetCurrencyByIdQuery>(), default).Returns(currency);

            // Act
            var result = await controller.GetCurrencyById(currency.Id.ToString());

            // Assert
            await mediator.Received(1).Send(Arg.Any<GetCurrencyByIdQuery>(), default);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CurrencyDto>(result);
        }

        [Test, ApplicationData]
        public async Task ShouldDeleteCurrencyQuery(
            [Frozen] IMediator mediator,
            [Greedy] CurrencyController controller)
        {
            var currencyId = Guid.NewGuid(); // Generate a new GUID for testing purposes

            mediator.Send(Arg.Any<DeleteCurrencyCommand>(), default).Returns(new CommandsStatus());

            // Act
            var result = await controller.DeleteCurrency(currencyId.ToString());

            // Assert
            await mediator.Received(1).Send(Arg.Is<DeleteCurrencyCommand>(cmd => cmd.Id == currencyId.ToString()), default);
            Assert.IsNotNull(result);
        }
    }
}

