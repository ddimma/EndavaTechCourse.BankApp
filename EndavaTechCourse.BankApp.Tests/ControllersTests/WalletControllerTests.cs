using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using EndavaTechCourse.BankApp.Shared;
using EndavaTechCourse.BankApp.Tests.Common;
using EndavaTechCourse.BankApp.Server.Controllers;
using EndavaTechCourse.BankApp.Domain.Models;
using AutoFixture.Idioms;
using MediatR;
using NSubstitute;
using EndavaTechCourse.BankApp.Application.Commands;
using EndavaTechCourse.BankApp.Application.Queries.GetWallets;
using EndavaTechCourse.BankApp.Application.Commands.AddWallet;
using EndavaTechCourse.BankApp.Application.Queries.GetWalletById;
using EndavaTechCourse.BankApp.Application.Commands.DeleteWallet;

namespace EndavaTechCourse.BankApp.Tests.ControllersTests
{
	public class WalletControllerTests
	{
        [Test, ApplicationData]
		public async Task ShouldSendGetWalletsQuery(
			[Frozen] IMediator mediator,
			[Greedy] WalletsController controller,
            Wallet firstWallet,
            Wallet secondWallet)
		{
            var wallets = new List<Wallet>();
            wallets.Append(firstWallet);
            wallets.Append(secondWallet);

            mediator.Send(Arg.Any<GetWalletsQuery>(), default).Returns(wallets);

            var result = await controller.GetWallets();

            await mediator.Received(1).Send(Arg.Any<GetWalletsQuery>(), default);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<WalletDto>>(result);
            //Assert.AreEqual(wallets.Count, result.Count);
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

            var result = controller.AddWallet(walletDto);

            Assert.IsNotNull(result);
        }

        [Test, ApplicationData]
        public async Task ShouldSendAddWalletsCommand(
            [Frozen] IMediator mediator,
            [Greedy] WalletsController controller,
            WalletDto walletDto)
        {
            mediator.Send(Arg.Any<AddWalletCommand>(), default).Returns(new CommandsStatus());

            await controller.AddWallet(walletDto);

            await mediator.Received(1).Send(Arg.Any<AddWalletCommand>(), default);
        }

        [Test, ApplicationData]
        public async Task ShouldSendGetWalletByIdQuery(
            [Frozen] IMediator mediator,
            [Greedy] WalletsController controller,
            Wallet wallet)
        {
            mediator.Send(Arg.Any<GetWalletByIdQuery>(), default).Returns(wallet);

            var result = await controller.GetWalletById(wallet.Id.ToString());

            await mediator.Received(1).Send(Arg.Any<GetWalletByIdQuery>(), default);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<WalletDto>(result);
        }

        [Test, ApplicationData]
        public async Task ShouldSendDeleteWalletCommand(
            [Frozen] IMediator mediator,
            [Greedy] WalletsController controller)
        {
            var walletId = Guid.NewGuid();
            mediator.Send(Arg.Any<DeleteWalletCommand>(), default).Returns(new CommandsStatus());

            var result = await controller.DeleteWallet(walletId.ToString());

            await mediator.Received(1).Send(Arg.Is<DeleteWalletCommand>(cmd => cmd.Id == walletId.ToString()), default);
            Assert.IsNotNull(result);
        }
    }
}

