using FastTechFoods.Kitchen.Application.Interfaces.Repository;
using FastTechFoods.Kitchen.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FastTechFoods.Kitchen.Infrastructure.Repository;
public class EmployeeRepository(ApplicationDbContext context) : EFRepository<Employee>(context), IEmployeeRepository
{
    public async Task<Employee?> GetByEmailAsync(string email)
        => await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
}
