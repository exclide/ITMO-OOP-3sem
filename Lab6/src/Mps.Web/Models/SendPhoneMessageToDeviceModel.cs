using Mps.Domain.ValueObjects;

namespace Mps.Web.Models;

public record SendPhoneMessageToDeviceModel(Guid DeviceId, MessageText MessageText, PhoneNumber PhoneNumber);