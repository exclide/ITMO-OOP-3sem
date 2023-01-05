using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mps.Application.Dtos;
using Mps.Application.EmployeeCQ;
using Mps.Web.Models;

namespace Mps.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BossEmployeeController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public BossEmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> CreateBossAsync([FromBody] CreateEmployeeModel model)
    {
        var command = new CreateBossEmployeeCommand(model.Login, model.Password, model.FullName);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response);
    }
}