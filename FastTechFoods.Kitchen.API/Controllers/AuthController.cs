using FastTechFoods.Kitchen.Application.Interfaces.Services.Authentication;
using FastTechFoods.Kitchen.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FastTechFoods.Kitchen.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService, IJwtTokenService tokens) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly IJwtTokenService _tokens = tokens;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel vm)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // DADOS ABAIXO SÓ ESTÃO AQUI PARA FACILITAR O TESTE DOS PROFESSORES. ÓBVIAMENTE QUE NÃO FICARIAM EM UM CENÁRIO REAL.
        
        // Login Manager
        //{
        //    "email": "manuel.manager@fasttechfoods.com",
        //    "password": "Manager123!"
        //}

        // Login Attendant
        //{
        //    "email": "maria.attendant@fasttechfoods.com",
        //    "password": "Attendant123!"
        //}

        var employee = await _authService.ValidateCredentialsAsync(vm.Email, vm.Password);
        if (employee is null)
            return Unauthorized("Invalid email or password.");

        // TODO: Gerar JWT e retornar aqui.
        var jwt = _tokens.GenerateToken(employee);

        return Ok(new { Message = "Successfully authenticated.", token = jwt });
    }
}
