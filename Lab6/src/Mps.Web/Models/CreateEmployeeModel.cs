using Mps.Domain.Department;
using Mps.Domain.ValueObjects;

namespace Mps.Web.Models;

public record CreateEmployeeModel(Account Account, FullName FullName);