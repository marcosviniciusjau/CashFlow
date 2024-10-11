using CashFlow.Domain.Entities.Enums;

namespace CashFlow.Domain.Entities;
public class User
{
    public long Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string ManagerName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    public string Role { get; set; } = Roles.ADMIN;
}
