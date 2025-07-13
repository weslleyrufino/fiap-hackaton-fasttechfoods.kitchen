using Bogus;
using FastTechFoods.Kitchen.Application.Interfaces.Repository;
using FastTechFoods.Kitchen.Application.Services;
using FastTechFoods.Kitchen.Domain.Entities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;

namespace FastTechFoods.Kitchen.Tests;

public class ContatoServiceTests
{
    private readonly ContatoService _contatoService;
    private readonly Mock<IContatoRepository> _mockContatoRepository;
    private readonly Faker<Contato> _contatoFaker;

    public ContatoServiceTests()
    {
        _mockContatoRepository = new Mock<IContatoRepository>();
        _contatoService = new ContatoService(_mockContatoRepository.Object);

        _contatoFaker = new Faker<Contato>()
            //.RuleFor(c => c.Id, f => f.IndexFaker + 1)
            .RuleFor(c => c.Name, f => f.Name.FullName())
            .RuleFor(c => c.Telefone, f => f.Phone.PhoneNumber("#########"))
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.RegiaoId, f => f.Random.Int(1, 100))
            .RuleFor(c => c.Regiao, f => new Regiao { Name = f.Locale, DDD = f.Random.Int(11, 99) });
    }

    [Fact(DisplayName = "Retornar Lista de Contatos")]
    [Trait("Category", "Unit")]
    [Trait("Contatos", "ContatoService")]
    public void GetContatos_DeveRetornarListaDeContatos()
    {
        // Arrange
        var contatos = _contatoFaker.Generate(2); // Gera 2 contatos aleatórios
        _mockContatoRepository.Setup(repo => repo.GetTodosContatosMesclandoComDDD()).Returns(contatos);

        // Act
        var resultado = _contatoService.GetContatos();

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count());
        _mockContatoRepository.Verify(repo => repo.GetTodosContatosMesclandoComDDD(), Times.Once);
    }

    [Fact(DisplayName = "Retornar Contatos com DDD")]
    [Trait("Category", "Unit")]
    [Trait("Contatos", "ContatoService")]
    public void GetContatosPorDDD_DeveRetornarContatosComDddEspecifico()
    {
        // Arrange
        var ddd = 11;
        var contatos = _contatoFaker.Generate(3).Select(c => { c.Regiao.DDD = ddd; return c; }).ToList();
        _mockContatoRepository.Setup(repo => repo.GetContatosPorDDD(ddd)).Returns(contatos);

        // Act
        var resultado = _contatoService.GetContatosPorDDD(ddd);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(3, resultado.Count());
        Assert.All(resultado, contato => Assert.Equal(ddd, contato.Regiao.DDD));
        _mockContatoRepository.Verify(repo => repo.GetContatosPorDDD(ddd), Times.Once);
    }

    [Fact(DisplayName = "Retornar Contato Específico")]
    [Trait("Category", "Unit")]
    [Trait("Contatos", "ContatoService")]
    public void ObterPorId_DeveRetornarContatoComIdEspecifico()
    {
        // Arrange
        var contato = _contatoFaker.Generate();
        _mockContatoRepository.Setup(repo => repo.ObterPorId(contato.Id)).Returns(contato);

        // Act
        var resultado = _contatoService.ObterPorId(contato.Id);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(contato.Id, resultado.Id);
        _mockContatoRepository.Verify(repo => repo.ObterPorId(contato.Id), Times.Once);
    }

    [Fact(DisplayName = "Chama Cadastrar")]
    [Trait("Category", "Unit")]
    [Trait("Contatos", "ContatoService")]
    public void PostInserirContato_DeveChamarCadastrar()
    {
        // Arrange
        var contato = _contatoFaker.Generate();

        // Act
        _contatoService.PostInserirContato(contato);

        // Assert
        _mockContatoRepository.Verify(repo => repo.Cadastrar(contato), Times.Once);
    }

    [Fact(DisplayName = "Chama Alterar")]
    [Trait("Category", "Unit")]
    [Trait("Contatos", "ContatoService")]
    public void PutAlterarContato_DeveChamarAlterar()
    {
        // Arrange
        var contato = _contatoFaker.Generate();

        // Act
        _contatoService.PutAlterarContato(contato);

        // Assert
        _mockContatoRepository.Verify(repo => repo.Alterar(contato), Times.Once);
    }

    [Fact(DisplayName = "Chama Deletar")]
    [Trait("Category", "Unit")]
    [Trait("Contatos", "ContatoService")]
    public void DeleteContato_DeveChamarDeletar()
    {
        // Arrange
        var id = _contatoFaker.Generate().Id;

        // Act
        _contatoService.DeleteContato(id);

        // Assert
        _mockContatoRepository.Verify(repo => repo.Deletar(id), Times.Once);
    }
}