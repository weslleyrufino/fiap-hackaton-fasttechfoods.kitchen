using FastTechFoods.Kitchen.Application.ViewModel.Base;
using System.ComponentModel.DataAnnotations;

namespace FastTechFoods.Kitchen.Application.ViewModel;

public class RegiaoViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The name is required.")]
    public required string Name { get; set; }

    public int DDD { get; set; }
}
