using FastTechFoods.Kitchen.Application.ViewModel.Base;
using FastTechFoods.Kitchen.Application.ViewModel.Order.Enum;
using System.ComponentModel.DataAnnotations;

namespace FastTechFoods.Kitchen.Application.ViewModel.Order;
public class OrderViewlModel : ViewModelBase
{
    [Required(ErrorMessage = "The customer ID is required.")]
    public Guid CustomerId { get; set; } // Ou CPF, se usar autenticação via CPF

    [Required(ErrorMessage = "The order date is required.")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "The order status is required.")]
    public EnumStatus Status { get; set; } // Ex: "Pending", "Accepted", "Rejected", "Canceled"

    [Required(ErrorMessage = "The delivery method is required.")]
    public string DeliveryMethod { get; set; } // Ex: "Counter", "DriveThru", "Delivery"

    /// <summary>
    /// Only Rejected or Canceled
    /// </summary>
    public EnumStatus? CancellationReason { get; set; }

    [Required]
    public List<OrderItem> Items { get; set; } = new();
}