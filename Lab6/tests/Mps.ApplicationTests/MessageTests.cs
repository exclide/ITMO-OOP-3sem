using Microsoft.EntityFrameworkCore;
using Mps.Application.DepartmentCQ;
using Mps.Application.DeviceCQ;
using Mps.Application.MessageCQ;
using Mps.Domain.Message;
using Mps.Domain.ValueObjects;
using Mps.Infrastructure;

namespace Mps.ApplicationTests;

public class MessageTests : IDisposable
{
    private readonly DatabaseContext _context;
    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    public MessageTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());

        _context = new DatabaseContext(dbContextOptions.Options);
    }

    [Fact]
    public async void SendMessageToDeviceReadProcessMessage_MessageStateChanged()
    {
        var command = new CreateDepartmentCommand(new DepartmentName("Test"));
        var handler = new CreateDepartmentHandler(_context);
        var departmentDto = await handler.Handle(command, _cancellationToken);

        var createDeviceCommand1 = new CreateAddEmailDeviceCommand(departmentDto.Id, new EmailAddress("test1@gmail.com"));
        var createDeviceCommandHandler1 = new CreateAddEmailDeviceHandler(_context);
        var deviceDto = await createDeviceCommandHandler1.Handle(createDeviceCommand1, _cancellationToken);

        var sendMessageToDeviceCommand =
            new CreateSendEmailMessageToDeviceCommand(deviceDto.Id, new MessageText("Text"), new EmailAddress("test@gmail.com"));
        var sendMessageToDeviceHandler = new CreateSendEmailMessageToDeviceHandler(_context);
        var messageDto = await sendMessageToDeviceHandler.Handle(sendMessageToDeviceCommand, _cancellationToken);

        var readMessageCommand = new ReadMessageCommand(messageDto.Id);
        var readMessageHandler = new MessageHandler(_context);
        await readMessageHandler.Handle(readMessageCommand, _cancellationToken);

        var message = _context.Messages.First(x => x.Id.Equals(messageDto.Id));
        Assert.Equal(MessageState.Read, message.MessageState);
    }
    
    
    

    public void Dispose()
    {
        _context.Dispose();
    }
}