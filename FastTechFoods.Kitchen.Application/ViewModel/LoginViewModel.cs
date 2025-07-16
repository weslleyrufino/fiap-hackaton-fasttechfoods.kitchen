using System.ComponentModel.DataAnnotations;

namespace FastTechFoods.Kitchen.Application.ViewModel;
public class LoginViewModel
{
    [Required(ErrorMessage = "The Email field is required.")]
    public required string Email { get; set; }


    [Required(ErrorMessage = "The Password field is required.")]
    public required string Password { get; set; }

}
