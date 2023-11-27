using Microsoft.AspNetCore.Mvc;
using EndavaTechCourse.BankApp.Shared;
using MediatR;
using EndavaTechCourse.BankApp.Application.Queries.GetWallets;
using EndavaTechCourse.BankApp.Application.Queries.GetWalletById;
using EndavaTechCourse.BankApp.Application.Commands.DeleteWallet;
using EndavaTechCourse.BankApp.Application.Commands.AddWallet;
using EndavaTechCourse.BankApp.Application.Commands.UpdateWallet;
using Microsoft.AspNetCore.Authorization;
using EndavaTechCourse.BankApp.Server.Common;
using EndavaTechCourse.BankApp.Server.Common.JWTToken;
using EndavaTechCourse.BankApp.Application.Queries.GetWalletsForUser;
using Mapper = EndavaTechCourse.BankApp.Server.Common.Mapper;
using EndavaTechCourse.BankApp.Application.Queries.GetFavoriteWallets;
using EndavaTechCourse.BankApp.Application.Commands.ToggleFavoriteWallet;

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
        [Authorize(Roles ="User")]
        public async Task <IActionResult> AddWallet([FromBody] WalletDto createWalletDTO)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Constants.UserIdClaimName);

            if (userIdClaim is null)
                return BadRequest();

            var userId = userIdClaim.Value;

            var command = new AddWalletCommand()
            {
                Type = createWalletDTO.Type,
                Amount = createWalletDTO.Amount,
                Currency = createWalletDTO.Currency,
                UserId = userId,
            };

            var result = await mediator.Send(command);

            return result.IsSuccessful ? Ok() : BadRequest(result.Error);
        }

        [HttpGet("{walletId}")]
        [Route("getById")]
        [Authorize(Roles ="User")]
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

        [HttpGet("favorites")]
        [Route("favorites")]
        [Authorize(Roles = "User")]
        public async Task<IEnumerable<WalletDto>> GetFavoriteWallet()
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Constants.UserIdClaimName);

            if (userIdClaim is null)
                return new List<WalletDto>();

            var userId = userIdClaim.Value;

            var query = new GetFavoriteWalletsQuery()
            {
                UserId = userId
            };

            var wallets = await mediator.Send(query);

            if (wallets == null)
            {
                return new List<WalletDto>();
            }

            return Mapper.Map(wallets);
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IEnumerable<WalletDto>> GetWallets()
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Constants.UserIdClaimName);

            if (userIdClaim is null)
                return new List<WalletDto>();

            var userId = userIdClaim.Value;

            var query = new GetWalletsForUserQuery()
            {
                UserId = userId,
            };

            var wallets = await mediator.Send(query);

            return Mapper.Map(wallets);
        }

        [HttpGet("all")]
        [Route("all")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IEnumerable<WalletDto>> GetAllWallets()
        {
            var wallets = await mediator.Send(new GetWalletsQuery());

            return Mapper.Map(wallets);
        }

        [HttpPost("{walletId}")]
        [Route("update")]
        [Authorize(Roles = "User")]
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
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> DeleteWallet(string walletId)
        {

            var walletToDelete = await mediator.Send(new DeleteWalletCommand
            {
                Id = walletId
            });
            return walletToDelete.IsSuccessful ? Ok() : BadRequest(walletToDelete.Error);
        }

        [HttpPost("toggle-favorite")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ToggleFavorite([FromBody] WalletDto wallet)
        {
            var command = new ToggleFavoriteWalletCommand()
            {
                Id = wallet.Id
            };

            var response = await mediator.Send(command);

            return response.IsSuccessful ? Ok() : BadRequest(response.Error);
        }
    }
}

