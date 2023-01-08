using Mps.Domain.Department;
using Mps.Domain.ValueObjects;

namespace Mps.Web.Models;

public record CreateEmployeeModel(string Login, string Password, FullName FullName);