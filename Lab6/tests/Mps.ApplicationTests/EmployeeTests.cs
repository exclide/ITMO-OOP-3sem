using Microsoft.EntityFrameworkCore;
using Mps.Application.DepartmentCQ;
using Mps.Application.DeviceCQ;
using Mps.Application.EmployeeCQ;
using Mps.Application.MessageCQ;
using Mps.Application.ReportCQ;
using Mps.Domain.ValueObjects;
using Mps.Infrastructure;

namespace Mps.ApplicationTests;

public class EmployeeTests : IDisposable
{
    private readonly DatabaseContext _context;
    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    public EmployeeTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

        _context = new DatabaseContext(dbContextOptions.Options);
    }

    [Fact]
    public async void CreateEmployee_DepartmentExists()
    {
        var command = new CreateEmployeeCommand("pleb", "123", new FullName("Kek", "Lol"));
        var handler = new CreateEmployeeHandler(_context);

        await handler.Handle(command, _cancellationToken);
        var employee = _context.Employees.First(x => x.Account.AccountLogin.Login.Equals("pleb"));

        Assert.NotNull(employee);
    }
    
    [Fact]
    public async void CreateDepartmentAddEmployeeAddDevicesAddMessagesGetDevicesGetMessagesFromDevices_DevicesExistAndMessagesExist()
    {
        var command = new CreateDepartmentCommand(new DepartmentName("Test"));
        var handler = new CreateDepartmentHandler(_context);
        var departmentDto = await handler.Handle(command, _cancellationToken);

        var createDeviceCommand1 = new CreateAddEmailDeviceCommand(departmentDto.Id, new EmailAddress("test1@gmail.com"));
        var createDeviceCommand2 = new CreateAddEmailDeviceCommand(departmentDto.Id, new EmailAddress("test2@gmail.com"));
        var createDeviceCommandHandler = new CreateAddEmailDeviceHandler(_context);
        var deviceDto1 = await createDeviceCommandHandler.Handle(createDeviceCommand1, _cancellationToken);
        var deviceDto2 = await createDeviceCommandHandler.Handle(createDeviceCommand2, _cancellationToken);
        
        var createEmployeeCommand = new CreateBossEmployeeCommand("boss", "123", new FullName("Kek", "Lol"));
        var createEmployeeCommandHandler = new CreateBossEmployeeHandler(_context);
        var employeeDto = await createEmployeeCommandHandler.Handle(createEmployeeCommand, _cancellationToken);

        var addEmployeeCommand = new SetDepartmentBossCommand(departmentDto.Id, employeeDto.Id);
        var addEmployeeHandler = new SetDepartmentBossHandler(_context);
        await addEmployeeHandler.Handle(addEmployeeCommand, _cancellationToken);

        var getControlledDevicesQuery = new GetControlledDevicesQuery(employeeDto.Id);
        var getControlledDevicesHandler = new GetControlledDevicesHandler(_context);

        var devices = await getControlledDevicesHandler.Handle(getControlledDevicesQuery, _cancellationToken);

        Assert.Equal(2, devices.Count);

        var sendMessageToDeviceCommand = new CreateSendEmailMessageToDeviceCommand(
            deviceDto1.Id,
            new MessageText("Test"), new EmailAddress("test@gmail.com"));
        var sendMessageToDeviceHandler = new CreateSendEmailMessageToDeviceHandler(_context);
        var messageDto = await sendMessageToDeviceHandler.Handle(sendMessageToDeviceCommand, _cancellationToken);
        
        var getMessagesFromDeviceQuery = new GetMessagesFromDeviceQuery(deviceDto1.Id);
        var getMessagesFromDeviceHandler = new GetMessagesFromDeviceHandler(_context);

        var messages = await getMessagesFromDeviceHandler.Handle(getMessagesFromDeviceQuery, _cancellationToken);

        Assert.NotEmpty(messages);
    }

    [Fact]
    public async void FormReportGetReport_CanFormReportAndReportExists()
    {
        var command = new CreateDepartmentCommand(new DepartmentName("Test"));
        var handler = new CreateDepartmentHandler(_context);
        var departmentDto = await handler.Handle(command, _cancellationToken);

        var createDeviceCommand = new CreateAddEmailDeviceCommand(departmentDto.Id, new EmailAddress("test1@gmail.com"));
        var createDeviceCommandHandler = new CreateAddEmailDeviceHandler(_context);
        var deviceDto1 = await createDeviceCommandHandler.Handle(createDeviceCommand, _cancellationToken);

        var createEmployeeCommand = new CreateBossEmployeeCommand("boss", "123", new FullName("Kek", "Lol"));
        var createEmployeeCommandHandler = new CreateBossEmployeeHandler(_context);
        var employeeDto = await createEmployeeCommandHandler.Handle(createEmployeeCommand, _cancellationToken);

        var addEmployeeCommand = new SetDepartmentBossCommand(departmentDto.Id, employeeDto.Id);
        var addEmployeeHandler = new SetDepartmentBossHandler(_context);
        await addEmployeeHandler.Handle(addEmployeeCommand, _cancellationToken);
        
        var sendMessageToDeviceCommand1 = new CreateSendEmailMessageToDeviceCommand(
            deviceDto1.Id,
            new MessageText("Test1"), new EmailAddress("test@gmail.com"));
        var sendMessageToDeviceCommand2 = new CreateSendEmailMessageToDeviceCommand(
            deviceDto1.Id,
            new MessageText("Test2"), new EmailAddress("test@gmail.com"));
        var sendMessageToDeviceCommand3 = new CreateSendEmailMessageToDeviceCommand(
            deviceDto1.Id,
            new MessageText("Test3"), new EmailAddress("test@gmail.com"));
        var sendMessageToDeviceHandler = new CreateSendEmailMessageToDeviceHandler(_context);
        var messageDto1 = await sendMessageToDeviceHandler.Handle(sendMessageToDeviceCommand1, _cancellationToken);
        var messageDto2 = await sendMessageToDeviceHandler.Handle(sendMessageToDeviceCommand2, _cancellationToken);
        var messageDto3 = await sendMessageToDeviceHandler.Handle(sendMessageToDeviceCommand3, _cancellationToken);
        
        var readMessageCommand = new ReadMessageCommand(messageDto1.Id);
        var processMessageCommand = new ProcessMessageCommand(messageDto2.Id);
        var messageHandler = new MessageHandler(_context);
        await messageHandler.Handle(readMessageCommand, _cancellationToken);
        await messageHandler.Handle(processMessageCommand, _cancellationToken);

        var formReportCommand = new FormAddReportCommand(departmentDto.Id, DateTime.UtcNow.AddDays(-1));
        var formReportHandler = new FormAddReportHandler(_context);

        var reportDto = await formReportHandler.Handle(formReportCommand, _cancellationToken);

        Assert.Equal(3, reportDto.MessagesTotal.Count);
        Assert.Equal(1, reportDto.MessagesProcessed.Count);
        Assert.Equal(1, reportDto.MessagesRead.Count);
        
        await formReportHandler.Handle(formReportCommand, _cancellationToken);
        await formReportHandler.Handle(formReportCommand, _cancellationToken);

        var getReportFromDateQuery = new GetReportsFromDateQuery(departmentDto.Id, DateTime.UtcNow.AddMonths(-5));
        var getReportHandler = new GetReportsQueryHandler(_context);

        var reports = await getReportHandler.Handle(getReportFromDateQuery, _cancellationToken);
        Assert.Equal(3, reports.Count);

        var getReportByIdQuery = new GetReportByIdQuery(departmentDto.Id, reportDto.Id);
        var reportDto2 = await getReportHandler.Handle(getReportByIdQuery, _cancellationToken);
        Assert.Equal(reportDto.Id, reportDto2.Id);

    }
    
    

    public void Dispose()
    {
        _context.Dispose();
    }
}