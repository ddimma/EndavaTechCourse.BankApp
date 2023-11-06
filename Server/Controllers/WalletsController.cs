using System;
using EndavaTechCourse.BankApp.Domain.Models;
using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using EndavaTechCourse.BankApp.Shared;
using Microsoft.EntityFrameworkCore;
using MediatR;
using EndavaTechCourse.BankApp.Application.Queries.GetWallets;

namespace EndavaTechCourse.BankApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletsController : ControllerBase
	{
        private readonly ApplicationDbContext _dbContext;
        public readonly IMediator mediator;
        public WalletsController(ApplicationDbContext dbContext, IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(dbContext);
            ArgumentNullException.ThrowIfNull(mediator);
            _dbContext = dbContext;
            this.mediator = mediator;
        }

        [HttpPost("create")]
        public IActionResult CreateWallet([FromBody] WalletDto createWalletDTO)
        {
            var wallet = new Wallet
            {
                Type = createWalletDTO.Type,
                Amount = createWalletDTO.Amount,
                Currency = new Currency
                {
                    CurrencyCode = createWalletDTO.Currency
                }
            };

            if (wallet == null)
            {
                return BadRequest();
            }

            _dbContext.Wallets.Add(wallet);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet("{walletId}")]
        public IActionResult GetWallet(Guid walletId)
        {
            var wallet = _dbContext.Wallets
            .Include(w => w.Currency) 
            .FirstOrDefault(w => w.Id == walletId);

            if (wallet == null)
            {
                return NotFound("Wallet not found.");
            }

            return Ok(wallet);
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
    }
}

