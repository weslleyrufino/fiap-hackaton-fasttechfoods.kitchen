using FastTechFoods.Kitchen.Application.ViewModel.Base;

namespace FastTechFoods.Kitchen.Application.ViewModel;

public class ContatoViewModel : ViewModelBase
{
    public string Telefone { get; set; }
    public string Email { get; set; }
    public int RegiaoId { get; set; }
    public RegiaoViewModel Regiao { get; set; }
}
