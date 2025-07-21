using FastTechFoods.Kitchen.Domain.Entities.Base;
using FastTechFoods.Kitchen.Domain.Entities.Enum;
using System.ComponentModel.DataAnnotations;

namespace FastTechFoods.Kitchen.Application.ViewModel.Order;
public class UpdateStatusOrderViewModel : EntityBase
{
    [Required(ErrorMessage = "The new status is required.")]
    [AllowedValues([EnumStatus.Accepted, EnumStatus.Rejected], ErrorMessage = $"Enter 1 (Accepted) or 2 (Rejected) status")]
    public EnumStatus Status { get; set; }

    public string? CancellationReason { get; set; }
}
