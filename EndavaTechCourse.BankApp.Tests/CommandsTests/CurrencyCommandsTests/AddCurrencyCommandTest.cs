using EndavaTechCourse.BankApp.Application.Commands.AddCurrency;
using EndavaTechCourse.BankApp.Domain.Models;
using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using EndavaTechCourse.BankApp.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace EndavaTechCourse.BankApp.Tests.CommandsTests.CurrencyCommandsTests
{
	public class AddCurrencyCommandTest
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

        [Test, ApplicationData]
        public async Task Handle_WhenCurrencyExists_ShouldReturnFailedStatus(
            [Frozen] ApplicationDbContext context,
            AddCurrencyCommand command)
        {
            var existingCurrency = new Currency
            {
                Name = command.Name,
                CurrencyCode = command.CurrencyCode,
                ChangeRate = 6
            };

            // Add the existing currency to the database
            await context.Currencies.AddAsync(existingCurrency);
            await context.SaveChangesAsync();

            var handler = new AddCurrencyHandler(context);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccessful);

            // Verify that the existing currency is still in the database
            var existingCurrencyInDb = await context.Currencies.FirstOrDefaultAsync(c => c.Name == command.Name);
            Assert.IsNotNull(existingCurrencyInDb);
            Assert.AreEqual(existingCurrency.CurrencyCode, existingCurrencyInDb.CurrencyCode);
            Assert.AreEqual(existingCurrency.ChangeRate, existingCurrencyInDb.ChangeRate);
        }
    }
}

