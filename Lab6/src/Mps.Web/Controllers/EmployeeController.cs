using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mps.Application.Dtos;
using Mps.Application.EmployeeCQ;
using Mps.Web.Constants;
using Mps.Web.Models;

namespace Mps.Web.Controllers;

[ApiController]
[Route("api/employee")]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("create-pleb")]
    public async Task<ActionResult<EmployeeDto>> CreatePlebAsync([FromBody] CreateEmployeeModel model)
    {
        var command = new CreateEmployeeCommand(model.Login, model.Password, model.FullName);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response);
    }
    
    [HttpPost("create-boss")]
    public async Task<ActionResult<EmployeeDto>> CreateBossAsync([FromBody] CreateEmployeeModel model)
    {
        var command = new CreateBossEmployeeCommand(model.Login, model.Password, model.FullName);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response);
    }
    
    [Authorize(Policy = PolicyName.PlebPolicy)]
    [HttpPost("{employeeId:guid}/get-controlled-devices")]
    public async Task<ActionResult<IReadOnlyCollection<EmployeeDto>>> GetControlledDevicesAsync(Guid employeeId)
    {
        var command = new GetControlledDevicesQuery(employeeId);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response);
    }
}