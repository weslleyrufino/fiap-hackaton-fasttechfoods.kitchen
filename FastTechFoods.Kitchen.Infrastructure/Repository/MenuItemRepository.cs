using FastTechFoods.Kitchen.Application.Interfaces.Repository;
using FastTechFoods.Kitchen.Domain.Entities;

namespace FastTechFoods.Kitchen.Infrastructure.Repository;
public class MenuItemRepository(ApplicationDbContext context) : EFRepository<MenuItem>(context), IMenuItemRepository
{
    
}
