using System;
using EndavaTechCourse.BankApp.Application.Commands.AddCurrency;
using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using EndavaTechCourse.BankApp.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace EndavaTechCourse.BankApp.Tests.CommandsTests.CurrencyCommandsTests
{
	public class DeleteCurrencyCommandTest
	{
        [Test, ApplicationData]
        public async Task Handle_WhenCurrencyDoesNotExist_ShouldAddCurrencyToDatabase(
            [Frozen] ApplicationDbContext context,
            AddCurrencyCommand command)
        {
            // Arrange
            var handler = new AddCurrencyHandler(context);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);

            var addedCurrency = await context.Currencies.FirstOrDefaultAsync(c => c.Name == command.Name);

            Assert.IsNotNull(addedCurrency);
            Assert.AreEqual(command.CurrencyCode, addedCurrency.CurrencyCode);
            Assert.AreEqual(command.ChangeRate, addedCurrency.ChangeRate);
        }
    }
}

