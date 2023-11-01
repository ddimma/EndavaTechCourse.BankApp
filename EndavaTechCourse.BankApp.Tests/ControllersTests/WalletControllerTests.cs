using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using EndavaTechCourse.BankApp.Shared;
using EndavaTechCourse.BankApp.Tests.Common;
using EndavaTechCourse.BankApp.Server.Controllers;
using EndavaTechCourse.BankApp.Domain.Models;
using FluentAssertions;
using AutoFixture.Idioms;
using AutoFixture.AutoMoq;
using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using System;


namespace EndavaTechCourse.BankApp.Tests.ControllersTests
{
	public class WalletControllerTests
	{
        [Test, ApplicationData]
		public async Task ShouldGetWallets(
			[Frozen] ApplicationDbContext context,
			[Greedy] WalletsController controller,
			Wallet firstWallet,
			Wallet secondWallet)
		{
			context.Wallets.AddRange(firstWallet, secondWallet);
			context.SaveChanges();
			context.ChangeTracker.Clear();

			var result = controller.GetWallets();

            //Assert
            result.Should().NotBeNull();
            var walletList = await result;
            walletList.Should().HaveCount(2);

		}

		[Test, ApplicationData]
        public void CanCreateInstance(GuardClauseAssertion assertion)
            => assertion.Verify(typeof(WalletsController).GetConstructors());

        [Test, ApplicationData]
        public void CreateWallet_ValidData_ReturnsOkResult(
            [Frozen] ApplicationDbContext dbContext,
            [Greedy] WalletsController controller,
            WalletDto walletDto,
            Wallet wallet)
        {
            dbContext.Wallets.Add(wallet);
            dbContext.SaveChanges();
            dbContext.ChangeTracker.Clear();

            var result = controller.CreateWallet(walletDto);

            Assert.IsNotNull(result);
        }

        [Test, ApplicationData]
        public void CreateWallet_ValidData_ReturnsBadRequest(
            [Frozen] ApplicationDbContext dbContext,
            [Greedy] WalletsController controller,
            WalletDto walletDto,
            Wallet wallet)
        {
            var walletDto = new WalletDto
            {

            };

            dbContext.Wallets.Add(wallet);
            dbContext.SaveChanges();
            dbContext.ChangeTracker.Clear();

            var result = controller.CreateWallet(walletDto);

            Assert.IsNull(result);
        }
    }
}

