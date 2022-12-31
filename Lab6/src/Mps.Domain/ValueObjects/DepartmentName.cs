using Mps.Domain.Primitives;

namespace Mps.Domain.ValueObjects;

public class DepartmentName : ValueObject
{
    public DepartmentName(string name)
    {
        Name = name;
    }

    public string Name { get; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}