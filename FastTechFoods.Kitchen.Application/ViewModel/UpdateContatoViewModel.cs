using FastTechFoods.Kitchen.Application.Validations;
using FastTechFoods.Kitchen.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace FastTechFoods.Kitchen.Application.ViewModel;

public class UpdateContatoViewModel : EntityBase
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
    [ValidaFormatoNumeroTelefones(ErrorMessage = "Formato de telefone inválido.")]
    public string Telefone { get; set; }

    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo RegiaoId é obrigatório.")]
    public int RegiaoId { get; set; }
}
