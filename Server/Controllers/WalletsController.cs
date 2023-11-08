﻿using System;
using EndavaTechCourse.BankApp.Domain.Models;
using EndavaTechCourse.BankApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using EndavaTechCourse.BankApp.Shared;
using Microsoft.EntityFrameworkCore;
using MediatR;
using EndavaTechCourse.BankApp.Application.Queries.GetWallets;
using EndavaTechCourse.BankApp.Application.Queries.GetWalletById;
using EndavaTechCourse.BankApp.Application.Commands.DeteleCurrency;
using EndavaTechCourse.BankApp.Application.Commands.DeleteWallet;

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
        public IActionResult AddWallet([FromBody] WalletDto createWalletDTO)
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

        [HttpPost("{walletId}")]
        public async Task<IActionResult> DeleteCurrency(string walletId)
        {
            var walletToDelete = await mediator.Send(new DeleteWalletCommand
            {
                Id = walletId
            });
            return walletToDelete.IsSuccessful ? Ok() : BadRequest(walletToDelete.Error);
        }
    }
}

