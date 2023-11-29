﻿using MediatR;
namespace EndavaTechCourse.BankApp.Application.Commands.DeleteCommision
{
	public class DeleteCommisionCommand : IRequest<CommandsStatus>
	{
		public string Id { get; set; }
	}
}

