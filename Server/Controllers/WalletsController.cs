using System;
using EndavaTechCourse.BankApp.Domain.Models;
using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using EndavaTechCourse.BankApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace EndavaTechCourse.BankApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletsController : ControllerBase
	{
        private readonly ApplicationDbContext _dbContext;
        public WalletsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult CreateWallet([FromBody] CreateWalletDTO createWalletDTO)
        {
            var wallet = new Wallet
            {
                Type = createWalletDTO.Type,
                Amount = createWalletDTO.Amount,
                Currency = new Currency
                {
                    Name = createWalletDTO.CurrencyDTO.Name,
                    CurrencyCode = createWalletDTO.CurrencyDTO.CurrencyCode,
                    ChangeRate = createWalletDTO.CurrencyDTO.ChangeRate
                }
            };

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
        public IEnumerable<GetWalletDTO> GetWallets()
        {
            var wallets = _dbContext.Wallets
            .Include(w => w.Currency)
            .ToList();

            var walletsList = new List<GetWalletDTO>();
            foreach (var wallet in wallets)
            {
                var newWallet = new GetWalletDTO
                {
                    Amount = wallet.Amount,
                    Type = wallet.Type,
                    Id = wallet.Id,
                    Currency = wallet.Currency.CurrencyCode,
                    CreateDate = wallet.TimeStamp
                };
                walletsList.Add(newWallet);
            }

            return walletsList;
        }


    }
}

