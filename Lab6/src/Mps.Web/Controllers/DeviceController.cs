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
[Route("api/device")]
public class DeviceController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public DeviceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [Authorize(Policy = PolicyName.PlebPolicy)]
    [HttpPost("{deviceId:guid}/get-messages-from-device")]
    public async Task<ActionResult<IReadOnlyCollection<MessageDto>>> SendEmailMessageAsync(Guid deviceId)
    {
        var command = new GetMessagesFromDeviceQuery(deviceId);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response);
    }
}