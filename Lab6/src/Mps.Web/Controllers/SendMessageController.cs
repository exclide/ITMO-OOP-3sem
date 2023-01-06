using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mps.Application.Dtos;
using Mps.Application.EmployeeCQ;
using Mps.Application.MessageCQ;
using Mps.Web.Models;

namespace Mps.Web.Controllers;

[ApiController]
[Route("api/send-message")]
public class SendMessageController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public SendMessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("send-email-message")]
    public async Task<ActionResult<MessageDto>> SendEmailMessageAsync([FromBody] SendEmailMessageToDeviceModel model)
    {
        var command = new CreateSendEmailMessageToDeviceCommand(model.DeviceId, model.MessageText, model.EmailAddress);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response);
    }
    
    [HttpPost("send-phone-message")]
    public async Task<ActionResult<MessageDto>> SendPhoneMessageAsync([FromBody] SendPhoneMessageToDeviceModel model)
    {
        var command = new CreateSendPhoneMessageToDeviceCommand(model.DeviceId, model.MessageText, model.PhoneNumber);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response);
    }
    
    [HttpPost("send-telegram-message")]
    public async Task<ActionResult<MessageDto>> SendTelegramMessageAsync([FromBody] SendTelegramMessageToDeviceModel model)
    {
        var command = new CreateSendTelegramMessageToDeviceCommand(model.DeviceId, model.MessageText, model.TelegramName);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response);
    }
}