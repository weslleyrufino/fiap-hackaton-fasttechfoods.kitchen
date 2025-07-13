using FastTechFoods.Kitchen.Application.ViewModel.Order.Enum;
using FastTechFoods.Kitchen.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace FastTechFoods.Kitchen.Application.ViewModel.Order;
public class UpdateStatusOrderViewModel : EntityBase
{
    [Required(ErrorMessage = "The new status is required.")]
    public EnumAcceptOrRejected Status { get; set; }

    public string? CancellationReason { get; set; }
}
