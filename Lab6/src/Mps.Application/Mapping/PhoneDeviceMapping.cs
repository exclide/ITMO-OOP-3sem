using Mps.Application.Dtos;
using Mps.Domain.Department;
using Mps.Domain.Device;

namespace Mps.Application.Mapping;

public static class PhoneDeviceMapping
{
    public static PhoneDeviceDto AsDto(this PhoneDevice phoneDevice)
        => new PhoneDeviceDto(phoneDevice.Id, phoneDevice.PhoneNumber);
}