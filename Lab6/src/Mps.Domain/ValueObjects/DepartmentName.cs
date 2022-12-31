using Mps.Domain.Exceptions;
using Mps.Domain.Primitives;

namespace Mps.Domain.ValueObjects;

public class DepartmentName : ValueObject
{
    public DepartmentName(string departmentName)
    {
        if (string.IsNullOrWhiteSpace(departmentName))
        {
            throw new MpsDomainExcpetion($"{nameof(departmentName)} was null or empty");
        }

        Name = departmentName;
    }

    public string Name { get; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}