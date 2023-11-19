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
using System.IdentityModel.Tokens.Jwt;
using EndavaTechCourse.BankApp.Server.Common.JWTToken;
using EndavaTechCourse.BankApp.Application.Queries.GetWalletsForUser;

namespace EndavaTechCourse.BankApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletsController : ControllerBase
	{
        public readonly IMediator mediator;
        private readonly IJwtService jwtService;

        public WalletsController(IMediator mediator, IJwtService jwtService)
        {
            ArgumentNullException.ThrowIfNull(mediator);
            ArgumentNullException.ThrowIfNull(jwtService);
            this.mediator = mediator;
            this.jwtService = jwtService;
        }

        [HttpPost("create")]
        //[Authorize]
        public async Task <IActionResult> AddWallet([FromBody] WalletDto createWalletDTO)
        {
            var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();

            var userId = jwtService.GetUserIdFromToken(authorizationHeader);

            var command = new AddWalletCommand()
            {
                Type = createWalletDTO.Type,
                Amount = createWalletDTO.Amount,
                Currency = createWalletDTO.Currency,
                UserId = userId
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
        //[Authorize(Roles = "User")]
        public async Task<List<WalletDto>> GetWallets()
        {
            var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();

            var userId = jwtService.GetUserIdFromToken(authorizationHeader);

            var query = new GetWalletsForUserQuery()
            {
                UserId = userId,
            };

            var wallets = await mediator.Send(query);
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

