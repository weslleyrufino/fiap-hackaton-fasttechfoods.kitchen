using FastTechFoods.Kitchen.Application.ViewModel.Base;
using FastTechFoods.Kitchen.Domain.Entities.Enum;
using System.ComponentModel.DataAnnotations;

namespace FastTechFoods.Kitchen.Application.ViewModel.Order;
public class OrderViewModel : ViewModelBase
{
    [Required(ErrorMessage = "The customer ID is required.")]
    public string CustomerId { get; set; } // string pois pode ser CPF ou email.

    [Required(ErrorMessage = "The order date is required.")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "The order status is required.")]
    public EnumStatus Status { get; set; } // Ex: "Pending", "Accepted", "Rejected", "Canceled"

    [Required(ErrorMessage = "The delivery method is required.")]
    public string DeliveryMethod { get; set; } // Ex: "Counter", "DriveThru", "Delivery"


    /// <summary>
    /// Only Rejected or Canceled
    /// </summary>
    public string? CancellationReason { get; set; }

    [Required]
    public List<OrderItemViewModel> Items { get; set; } = new();
}