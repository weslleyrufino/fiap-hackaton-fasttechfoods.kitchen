using FastTechFoods.Contracts;
using FastTechFoods.Kitchen.Domain.Entities;

namespace FastTechFoods.Kitchen.Application.ExtensionMethods.Event;
public static class MenuCreatedEventExtensions
{
    public static MenuCreatedEvent ToEventModel(this MenuItem menuItem)
    {
        return new MenuCreatedEvent
        (
            menuItem.Id,
            menuItem.Name,
            menuItem.Description,
            menuItem.Price,
            menuItem.Category,
            menuItem.IsAvailable,
            menuItem.CreatedAt
        );
    }
}