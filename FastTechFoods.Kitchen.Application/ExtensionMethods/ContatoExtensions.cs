using FastTechFoods.Kitchen.Application.ViewModel;
using FastTechFoods.Kitchen.Domain.Entities;

namespace FastTechFoods.Kitchen.Application.ExtensionMethods;

public static class ContatoExtensions
{
    public static Contato ToModel(this ContatoViewModel viewModel)
    {
        return new Contato
        {
            Id = viewModel.Id,
            Email = viewModel.Email,
            Nome = viewModel.Nome,
            Telefone = viewModel.Telefone,
            RegiaoId = viewModel.RegiaoId,
            Regiao = new Regiao()
            {
                Id = viewModel.Regiao.Id,
                DDD = viewModel.Regiao.DDD,
                Nome = viewModel.Regiao.Nome
            }
        };
    }

    public static Contato ToModel(this CreateContatoViewModel createViewModel)
    {
        return new Contato
        {
            Email = createViewModel.Email,
            Nome = createViewModel.Nome,
            Telefone = createViewModel.Telefone,
            RegiaoId = createViewModel.RegiaoId
        };
    }

    public static Contato ToModel(this UpdateContatoViewModel createViewModel)
    {
        return new Contato
        {
            Id = createViewModel.Id,
            Email = createViewModel.Email,
            Nome = createViewModel.Nome,
            Telefone = createViewModel.Telefone,
            RegiaoId = createViewModel.RegiaoId
        };
    }

    public static ContatoViewModel ToViewModel(this Contato model)
    {
        return new ContatoViewModel
        {
            Id = model.Id,
            Email = model.Email,
            Nome = model.Nome,
            Telefone = model.Telefone,
            RegiaoId = model.RegiaoId,
            Regiao = new RegiaoViewModel()
            {
                Id = model.Regiao.Id,
                DDD = model.Regiao.DDD,
                Nome = model.Regiao.Nome
            }
        };
    }

    public static IEnumerable<ContatoViewModel> ToViewModel(this IEnumerable<Contato> model)
        => model.Select(model => model.ToViewModel());

}
