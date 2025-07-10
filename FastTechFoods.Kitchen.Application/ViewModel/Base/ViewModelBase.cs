using System.ComponentModel.DataAnnotations;

namespace FastTechFoods.Kitchen.Application.ViewModel.Base;

public class ViewModelBase
{
    public int Id { get; set; }
    [Required(ErrorMessage = "The name is required.")]
    public required string Name { get; set; }
}
