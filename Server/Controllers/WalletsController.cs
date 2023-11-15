using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using EndavaTechCourse.BankApp.Shared;
using MediatR;
using EndavaTechCourse.BankApp.Application.Queries.GetWallets;
using EndavaTechCourse.BankApp.Application.Queries.GetWalletById;
using EndavaTechCourse.BankApp.Application.Commands.DeleteWallet;
using EndavaTechCourse.BankApp.Application.Commands.AddWallet;
using EndavaTechCourse.BankApp.Application.Commands.UpdateWallet;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using EndavaTechCourse.BankApp.Server.Common;

namespace EndavaTechCourse.BankApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletsController : ControllerBase
	{
        public readonly IMediator mediator;

        public WalletsController(ApplicationDbContext dbContext, IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(dbContext);
            ArgumentNullException.ThrowIfNull(mediator);
            this.mediator = mediator;
        }

        [HttpPost("create")]
        //[Authorize]
        public async Task <IActionResult> AddWallet([FromBody] WalletDto createWalletDTO)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Constants.UserIdClaimName);
            if (userIdClaim == null)
                return BadRequest();

            var userId = userIdClaim.Value;
            var command = new AddWalletCommand()
            {
                Type = createWalletDTO.Type,
                Amount = createWalletDTO.Amount,
                Currency = createWalletDTO.Currency,
                //UserId = userId
            };
            
            if (command == null)
            {
                return BadRequest();
            }

            var result = await mediator.Send(command);

            return result.IsSuccessful ? Ok() : BadRequest(result.Error);
        }

        [HttpGet("{walletId}")]
        [Route("getById")]
        public async Task<WalletDto> GetWalletById(string walletId)
        {
            var wallet = await mediator.Send(new GetWalletByIdQuery
            {
                Id = walletId
            });

            if (wallet == null)
            {
                return new WalletDto();
            }

            var walletDto = new WalletDto()
            {
                Amount = wallet.Amount,
                Id = wallet.Id.ToString(),
                Currency = wallet.Currency.CurrencyCode,
                Type = wallet.Type
            };
            return walletDto;
        }

        [HttpGet]
        public async Task<List<WalletDto>> GetWallets()
        {
            var wallets = await mediator.Send(new GetWalletsQuery());
            var walletsList = new List<WalletDto>();
            foreach (var wallet in wallets)
            {
                var newWallet = new WalletDto
                {
                    Amount = wallet.Amount,
                    Type = wallet.Type,
                    Id = wallet.Id.ToString(),
                    Currency = wallet.Currency.CurrencyCode,
                };
                walletsList.Add(newWallet);
            }
            return walletsList;
        }

        [HttpGet]
        public async Task<List<WalletDto>> GetWalletsForUser()
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Constants.UserIdClaimName);
            if (userIdClaim == null)
                return new List<WalletDto>();

            var userId = userIdClaim.Value; 
            var wallets = await mediator.Send(new GetWalletsQuery());
            var walletsList = new List<WalletDto>();
            foreach (var wallet in wallets)
            {
                var newWallet = new WalletDto
                {
                    Amount = wallet.Amount,
                    Type = wallet.Type,
                    Id = wallet.Id.ToString(),
                    Currency = wallet.Currency.CurrencyCode,
                };
                walletsList.Add(newWallet);
            }
            return walletsList;
        }

        [HttpPost("{walletId}")]
        [Route("update")]
        public async Task<IActionResult> UpdateWallet([FromBody] WalletDto walletDto)
        {
            var walletToUpdate = await mediator.Send(new UpdateWalletCommand
            {
                Id = walletDto.Id,
                Type = walletDto.Type,
                Amount = walletDto.Amount,
                Currency = walletDto.Currency
            });

            return walletToUpdate.IsSuccessful ? Ok() : BadRequest(walletToUpdate.Error);
        }

        [HttpPost("{walletId}")]
        [Route("delete")]
        public async Task<IActionResult> DeleteWallet(string walletId)
        {
            var walletToDelete = await mediator.Send(new DeleteWalletCommand
            {
                Id = walletId
            });
            return walletToDelete.IsSuccessful ? Ok() : BadRequest(walletToDelete.Error);
        }
    }
}

