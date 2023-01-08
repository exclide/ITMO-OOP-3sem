using Mps.Application.Dtos;
using Mps.Domain.Department;
using Mps.Domain.Device;

namespace Mps.Application.Mapping;

public static class BaseDeviceMapping
{
    public static BaseDeviceDto AsDto(this DeviceBase emailDevice)
        => new BaseDeviceDto(emailDevice.Id);
}