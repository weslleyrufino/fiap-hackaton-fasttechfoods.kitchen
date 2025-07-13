using FastTechFoods.Kitchen.Application.ViewModel.Base;
using System.ComponentModel.DataAnnotations;

namespace FastTechFoods.Kitchen.Application.ViewModel.Order;
public class OrderItem : ViewModelBase
{
    [Required(ErrorMessage = "The menu item ID is required.")]
    public Guid MenuItemId { get; set; }

    [Required(ErrorMessage = "The quantity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "The quantity must be at least 1.")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "The unit price is required.")]
    public decimal UnitPrice { get; set; }

    public decimal Total => UnitPrice * Quantity;
}
