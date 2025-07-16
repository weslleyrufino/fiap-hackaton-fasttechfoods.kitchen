using FastTechFoods.Kitchen.Domain.Entities;

namespace FastTechFoods.Kitchen.Application.Interfaces.Services.Authentication;
public interface IAuthService
{
    Task<Employee?> ValidateCredentialsAsync(string email, string password);
}
