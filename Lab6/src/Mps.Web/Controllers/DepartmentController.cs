using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mps.Application.DepartmentCQ;
using Mps.Application.Dtos;
using Mps.Domain.ValueObjects;
using Mps.Web.Models;

namespace Mps.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public DepartmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<DepartmentDto>> CreateAsync([FromBody] CreateDepartmentModel model)
    {
        var command = new CreateDepartmentCommand(model.DepartmentName);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response);
    }
}