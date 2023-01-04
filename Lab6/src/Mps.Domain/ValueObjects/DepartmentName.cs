using Mps.Domain.Exceptions;
using Mps.Domain.Primitives;

namespace Mps.Domain.ValueObjects;

public class DepartmentName : ValueObject
{
    public DepartmentName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new MpsDomainException($"{nameof(name)} was null or empty");
        }

        Name = name;
    }

    public string Name { get; private set; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}