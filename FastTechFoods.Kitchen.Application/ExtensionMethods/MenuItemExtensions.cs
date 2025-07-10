using FastTechFoods.Kitchen.Application.ViewModel.MenuItem;
using FastTechFoods.Kitchen.Domain.Entities;

namespace FastTechFoods.Kitchen.Application.ExtensionMethods;
public static class MenuItemExtensions
{

    public static MenuItem ToModel(this CreateMenuItemViewModel createViewModel)
    {
        return new MenuItem
        {
            Name = createViewModel.Name,
            Description = createViewModel.Description,
            Price = createViewModel.Price,
            Category = createViewModel.Category,
            IsAvailable = createViewModel.IsAvailable
        };
    }

    public static MenuItem ToModel(this UpdateMenuItemViewModel updateViewModel)
    {
        return new MenuItem
        {
            Id = updateViewModel.Id,
            Name = updateViewModel.Name,
            Description = updateViewModel.Description,
            Price = updateViewModel.Price,
            Category = updateViewModel.Category,
            IsAvailable = updateViewModel.IsAvailable
        };
    }
}
