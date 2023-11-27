using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EndavaTechCourse.BankApp.Application.Commands.Transaction;
using EndavaTechCourse.BankApp.Application.Queries.GetTransactions;
using EndavaTechCourse.BankApp.Application.Queries.GetWallets;
using EndavaTechCourse.BankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EndavaTechCourse.BankApp.Server.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionsController : ControllerBase
    {
        public readonly IMediator mediator;

        public TransactionsController(IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(mediator);
            this.mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTransactionToAnotherPerson([FromBody] TransactionDto transactionDto)
        {
            var command = new TransactionCommand()
            {
                Message = transactionDto.Message,
                SourceWalletId = transactionDto.SourceWalletId,
                DestinationWalletId = transactionDto.DestinationWalletId,
                TransactionAmount = transactionDto.TransactionAmount,
                DestinationWalletCodeOrEmail = transactionDto.DestinationWalletCodeOrEmail
            };

            if(command == null)
            {
                return BadRequest();
            }

            var result = await mediator.Send(command);

            return result.IsSuccessful ? Ok() : BadRequest(result.Error);
        }

        [HttpGet]
        public async Task<List<TransactionDto>> GetTransactions()
        {
            var transactions = await mediator.Send(new GetTransactionsQuery());
            var transactionsList = new List<TransactionDto>();
            foreach (var transaction in transactions)
            {
                var newTransaction = new TransactionDto
                {
                    Message = transaction.Message,
                    SourceWalletId = transaction.SourceWalletId.ToString(),
                    DestinationWalletId = transaction.DestinationWalletId.ToString(),
                    TransactionAmount = transaction.TransactionAmount,

                };
                transactionsList.Add(newTransaction);
            }
            return transactionsList;
        }
    }
}

