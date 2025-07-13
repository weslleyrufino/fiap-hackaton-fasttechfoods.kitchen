using FastTechFoods.Kitchen.Application.ViewModel.Base;
using System.ComponentModel.DataAnnotations;

namespace FastTechFoods.Kitchen.Application.ViewModel;

public class ContatoViewModel : ViewModelBase
{
    [Required(ErrorMessage = "The name is required.")]
    public required string Name { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public int RegiaoId { get; set; }
    public RegiaoViewModel Regiao { get; set; }
}
