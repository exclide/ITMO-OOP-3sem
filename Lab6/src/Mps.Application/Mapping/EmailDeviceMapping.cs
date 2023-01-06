using Mps.Application.Dtos;
using Mps.Domain.Department;
using Mps.Domain.Device;

namespace Mps.Application.Mapping;

public static class EmailDeviceMapping
{
    public static EmailDeviceDto AsDto(this EmailDevice emailDevice)
        => new EmailDeviceDto(emailDevice.Id, emailDevice.EmailAddress);
}