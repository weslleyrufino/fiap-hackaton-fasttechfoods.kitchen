using FastTechFoods.Kitchen.Domain.Entities.Base;

namespace FastTechFoods.Kitchen.Domain.Entities;
public class Employee : EntityBase
{
    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string Role { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
