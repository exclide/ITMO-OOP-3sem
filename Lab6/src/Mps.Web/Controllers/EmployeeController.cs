using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mps.Application.Dtos;
using Mps.Application.EmployeeCQ;
using Mps.Web.Models;

namespace Mps.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> CreateAsync([FromBody] CreateEmployeeModel model)
    {
        var command = new CreateEmployeeCommand(model.Account, model.FullName);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response);
    }
}