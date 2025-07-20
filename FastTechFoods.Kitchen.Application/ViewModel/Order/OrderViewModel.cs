using FastTechFoods.Kitchen.Application.ViewModel.Base;
using FastTechFoods.Kitchen.Domain.Entities.Enum;
using System.ComponentModel.DataAnnotations;

namespace FastTechFoods.Kitchen.Application.ViewModel.Order;
public class OrderViewModel : ViewModelBase
{
    public Guid CustomerId { get; set; } 

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public EnumStatus Status { get; set; } // Ex: "Pending", "Accepted", "Rejected", "Canceled"

    public EnumDeliveryMethod DeliveryMethod { get; set; }


    /// <summary>
    /// Only Rejected or Canceled
    /// </summary>
    public string? CancellationReason { get; set; }

    [Required]
    public List<OrderItemViewModel> Items { get; set; } = new();
}