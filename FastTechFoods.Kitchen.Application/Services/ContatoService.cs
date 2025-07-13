using FastTechFoods.Kitchen.Domain.Entities;
using FastTechFoods.Kitchen.Application.Interfaces.Repository;
using FastTechFoods.Kitchen.Application.Interfaces.Services;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace FastTechFoods.Kitchen.Application.Services;
public class ContatoService(IContatoRepository contatoRepository) : IContatoService
{
    private readonly IContatoRepository _contatoRepository = contatoRepository;

    public void DeleteContato(Guid id)
    {
        var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
        using var connection = factory.CreateConnection();
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(
                queue: "deletar_contato",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            string message = JsonSerializer
                .Serialize(id);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: "",
                routingKey: "deletar_contato",
                basicProperties: null,
                body: body);
        }
    }

    public IEnumerable<Contato> GetContatos() 
        => _contatoRepository.GetTodosContatosMesclandoComDDD();

    public IEnumerable<Contato> GetContatosPorDDD(int ddd) 
        => _contatoRepository.GetContatosPorDDD(ddd);

    public Contato ObterPorId(Guid id) 
        => _contatoRepository.ObterPorId(id);

    public void PostInserirContato(Contato contato)
    {
        var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
        using var connection = factory.CreateConnection();
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(
                queue: "inserir_contato",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            
            string message = JsonSerializer
                .Serialize(
                new Contato(){
                    Id = contato.Id, 
                    Name = contato.Name, 
                    Telefone = contato.Telefone, 
                    Email = contato.Email, 
                    RegiaoId = contato.RegiaoId 
                });
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: "",
                routingKey: "inserir_contato",
                basicProperties: null,
                body: body);
        }
    }

    public void PutAlterarContato(Contato contato)
    {
        var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
        using var connection = factory.CreateConnection();
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(
                queue: "alterar_contato",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            string message = JsonSerializer
                .Serialize(
                new Contato()
                {
                    Id = contato.Id,
                    Name = contato.Name,
                    Telefone = contato.Telefone,
                    Email = contato.Email,
                    RegiaoId = contato.RegiaoId
                });
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: "",
                routingKey: "alterar_contato",
                basicProperties: null,
                body: body);
        }
    }

}
