using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mps.Application.Dtos;
using Mps.Application.EmployeeCQ;
using Mps.Application.MessageCQ;
using Mps.Web.Constants;
using Mps.Web.Models;

namespace Mps.Web.Controllers;

[ApiController]
[Route("api/message")]
[Authorize(Policy = PolicyName.PlebPolicy)]
public class ProcessMessageController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ProcessMessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("{messageId:guid}/read-message")]
    public async Task<ActionResult<MessageDto>> ReadMessage(Guid messageId)
    {
        var command = new ReadMessageCommand(messageId);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response);
    }
    
    [HttpPost("{messageId:guid}/process-message")]
    public async Task<ActionResult<MessageDto>> ProcessMessage(Guid messageId)
    {
        var command = new ProcessMessageCommand(messageId);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response);
    }
}