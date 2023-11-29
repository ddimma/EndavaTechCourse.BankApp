using EndavaTechCourse.BankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EndavaTechCourse.BankApp.Application.Commands.AddCommision;
using EndavaTechCourse.BankApp.Application.Queries.GetCommisions;
using EndavaTechCourse.BankApp.Application.Commands.UpdateCommision;
using EndavaTechCourse.BankApp.Application.Commands.DeleteCommision;

namespace EndavaTechCourse.BankApp.Server.Controllers
{
    [Route("api/commisions")]
    [ApiController]
    public class CommisionController : ControllerBase
    {
        public readonly IMediator mediator;

        public CommisionController(IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(mediator);
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCommision([FromBody] CommisionDto dto)
        {
            var command = new AddCommisionCommand()
            {
                WalletType = dto.WalletType,
                CommisionRate = dto.CommisionRate,
            };

            var result = await mediator.Send(command);

            return result.IsSuccessful ? Ok() : BadRequest(result.Error);
        }

        [HttpGet]
        public async Task<IEnumerable<CommisionDto>> GetCommisions()
        {
            var commisions = await mediator.Send(new GetCommisionsQuery());
            var commisionsList = new List<CommisionDto>();
            foreach (var commision in commisions)
            {
                var newCommision = new CommisionDto
                {
                    WalletType = commision.WalletType,
                    CommisionRate = commision.CommisionRate
                };
                commisionsList.Add(newCommision);
            }
            return commisionsList;
        }


        [HttpPost("{commisionId}")]
        [Route("update")]
        public async Task<IActionResult> UpdateCommision([FromBody] CommisionDto dto)
        {
            var command = new UpdateCommisionCommand()
            {
                CommisionRate = dto.CommisionRate,
                WalletType = dto.WalletType
            };
            var commisionToUpdate = await mediator.Send(command);

            return commisionToUpdate.IsSuccessful ? Ok() : BadRequest(commisionToUpdate.Error);
        }

        [HttpPost("{currencyId}")]
        [Route("delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCommision(string commisionId)
        {
            var command = new DeleteCommisionCommand()
            {
                Id = commisionId
            };
            var currencyToDelete = await mediator.Send(command);

            return currencyToDelete.IsSuccessful ? Ok() : BadRequest(currencyToDelete.Error);
        }
    }
}

