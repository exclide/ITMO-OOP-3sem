using Mps.Domain.ValueObjects;

namespace Mps.Web.Models;

public record SendEmailMessageToDeviceModel(Guid DeviceId, MessageText MessageText, EmailAddress EmailAddress);