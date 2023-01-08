using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mps.Application.DepartmentCQ;
using Mps.Application.DeviceCQ;
using Mps.Application.Dtos;
using Mps.Application.ReportCQ;
using Mps.Domain.ValueObjects;
using Mps.Web.Constants;
using Mps.Web.Models;

namespace Mps.Web.Controllers;

[ApiController]
[Route("api/department")]
public class DepartmentController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public DepartmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("create")]
    public async Task<ActionResult<DepartmentDto>> CreateAsync([FromBody] CreateDepartmentModel model)
    {
        var command = new CreateDepartmentCommand(model.DepartmentName);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response);
    }
    
    [HttpPost("{departmentId:guid}/set-boss")]
    public async Task<ActionResult<DepartmentDto>> SetBoss(Guid departmentId, Guid bossId)
    {
        var command = new SetDepartmentBossCommand(departmentId, bossId);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response);
    }
    
    [Authorize(Policy = PolicyName.BossPolicy)]
    [HttpPost("{departmentId:guid}/add-employee")]
    public async Task<ActionResult<DepartmentDto>> AddEmployee(Guid departmentId, Guid employeeId)
    {
        var command = new AddDepartmentEmployeeCommand(departmentId, employeeId);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response);
    }
    
    [Authorize(Policy = PolicyName.BossPolicy)]
    [HttpPost("{departmentId:guid}/create-add-email-device")]
    public async Task<ActionResult<EmailDeviceDto>> CreateAddEmailDevice(Guid departmentId, EmailAddress emailAddress)
    {
        var command = new CreateAddEmailDeviceCommand(departmentId, emailAddress);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response);
    }
    
    [Authorize(Policy = PolicyName.BossPolicy)]
    [HttpPost("{departmentId:guid}/create-add-phone-device")]
    public async Task<ActionResult<PhoneDeviceDto>> CreateAddPhoneDevice(Guid departmentId, PhoneNumber phoneNumber)
    {
        var command = new CreateAddPhoneDeviceCommand(departmentId, phoneNumber);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response);
    }
    
    [Authorize(Policy = PolicyName.BossPolicy)]
    [HttpPost("{departmentId:guid}/create-add-telegram-device")]
    public async Task<ActionResult<TelegramDeviceDto>> CreateAddTelegramDevice(Guid departmentId, TelegramName telegramName)
    {
        var command = new CreateAddTelegramDeviceCommand(departmentId, telegramName);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response);
    }
    
    [Authorize(Policy = PolicyName.BossPolicy)]
    [HttpPost("{departmentId:guid}/get-reports-from-date")]
    public async Task<ActionResult<IReadOnlyCollection<ReportDto>>> GetReportsFromDate(Guid departmentId, DateTime fromDate)
    {
        var command = new GetReportsFromDateQuery(departmentId, fromDate);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response);
    }
    
    [Authorize(Policy = PolicyName.BossPolicy)]
    [HttpPost("{departmentId:guid}/get-report-by-id")]
    public async Task<ActionResult<ReportDto>> GetReportById(Guid departmentId, Guid reportId)
    {
        var command = new GetReportByIdQuery(departmentId, reportId);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response);
    }
    
    [Authorize(Policy = PolicyName.BossPolicy)]
    [HttpPost("{departmentId:guid}/form-add-report")]
    public async Task<ActionResult<ReportDto>> FormAddReport(Guid departmentId, DateTime messageDate)
    {
        var command = new FormAddReportCommand(departmentId, messageDate);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response);
    }
}