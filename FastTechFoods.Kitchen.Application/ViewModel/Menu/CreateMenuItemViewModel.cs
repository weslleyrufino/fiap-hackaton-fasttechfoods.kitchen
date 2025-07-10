using System.ComponentModel.DataAnnotations;

namespace FastTechFoods.Kitchen.Application.ViewModel;
public class CreateMenuItemViewModel
{
    [Required(ErrorMessage = "The name is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "The description is required.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "The price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "The price must be greater than zero.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "The category is required.")]
    public string Category { get; set; }

    [Required(ErrorMessage = "The availability status is required.")]
    public bool IsAvailable { get; set; }
}
