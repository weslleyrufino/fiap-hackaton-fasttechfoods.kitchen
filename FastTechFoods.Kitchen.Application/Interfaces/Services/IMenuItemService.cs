using FastTechFoods.Kitchen.Application.ViewModel.MenuItem;

namespace FastTechFoods.Kitchen.Application.Interfaces.Services;
public interface IMenuItemService
{
    Task CreateMenuItemAsync(CreateMenuItemViewModel menuItem);
    Task UpdateMenuItemAsync(UpdateMenuItemViewModel menuItem);
    Task<bool> ExistsAsync(Guid id);
}
