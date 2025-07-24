using FastTechFoods.Contracts;
using FastTechFoods.Kitchen.Domain.Entities;

namespace FastTechFoods.Kitchen.Application.ExtensionMethods.Event;
public static class MenuUpdatedEventExtensions
{
    public static MenuUpdatedEvent ToUpdateEventModel(this MenuItem menuItem)
    {
        return new MenuUpdatedEvent
        (
            menuItem.Id,
            menuItem.Name,
            menuItem.Description,
            menuItem.Price,
            menuItem.Category,
            menuItem.IsAvailable,
            menuItem.UpdatedAt
        );
    }
}
