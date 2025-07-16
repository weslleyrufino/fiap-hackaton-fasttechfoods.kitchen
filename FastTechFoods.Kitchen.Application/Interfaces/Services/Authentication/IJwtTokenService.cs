using FastTechFoods.Kitchen.Domain.Entities;

namespace FastTechFoods.Kitchen.Application.Interfaces.Services.Authentication;
public interface IJwtTokenService
{
    string GenerateToken(Employee employee);
}
