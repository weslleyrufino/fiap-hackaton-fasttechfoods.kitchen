using FastTechFoods.Kitchen.Application.ExtensionMethods;
using FastTechFoods.Kitchen.Application.Interfaces.Services;
using FastTechFoods.Kitchen.Application.ViewModel;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace FastTechFoods.Kitchen.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MenuController(IContatoService contatoService, ILogger<MenuController> logger, IBus bus, IConfiguration configuration) : ControllerBase
{
    private readonly IContatoService _contatoService = contatoService;
    private readonly ILogger<MenuController> _logger = logger;
    private readonly IBus _bus = bus;
    private readonly IConfiguration _configuration = configuration;


    [HttpGet]
    public IActionResult Get()
    {
        var contatos = _contatoService.GetContatos().ToViewModel();

        if (!contatos.Any())
            return NoContent();

        return Ok(contatos);
    }

    [HttpGet("{ddd:int}")]
    public IActionResult ConsultaPorDDD([FromRoute] int ddd)
    {
        var contatos = _contatoService.GetContatosPorDDD(ddd)?.ToViewModel();

        if (contatos == null || !contatos.Any())
            return NotFound("Nenhum contato encontrado para o DDD especificado.");

        return Ok(contatos);
    }

    [HttpPost]
    public async Task<IActionResult> PostInserirContato([FromBody] CreateContatoViewModel contato)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var nomeFila = _configuration
            .GetSection("MassTransit_InserirContato")["NomeFila"] ?? string.Empty;

        var endpoint = await _bus
            .GetSendEndpoint(new Uri($"queue:{nomeFila}"));

        await endpoint.Send(contato.ToModel());

        _contatoService.PostInserirContato(contato.ToModel());

        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> PutAlterarContato([FromBody] UpdateContatoViewModel contato)
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Aqui não deverá mais obter direto da base de dados. Deverá obter pelo azure function.
        if (_contatoService.ObterPorId(contato.Id) is null)
            return NotFound("Contato não existe");

        var nomeFila = _configuration
            .GetSection("MassTransit_AlterarContato")["NomeFila"] ?? string.Empty;

        var endpoint = await _bus
            .GetSendEndpoint(new Uri($"queue:{nomeFila}"));

        await endpoint.Send(contato.ToModel());

        //_contatoService.PutAlterarContato(contato.ToModel());
        return NoContent();

    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteContato([FromRoute] int id)
    {
        var nomeFila = _configuration
            .GetSection("MassTransit_DeletarContato")["NomeFila"] ?? string.Empty;

        var endpoint = await _bus
            .GetSendEndpoint(new Uri($"queue:{nomeFila}"));

        await endpoint.Send(id);// não funcinou mandar o id.

        //_contatoService.DeleteContato(id);

        return NoContent();
    }
}
