using FastTechFoods.Contracts;
using FastTechFoods.Contracts.Enum;
using FastTechFoods.Kitchen.Application.ViewModel.Order;
using FastTechFoods.Kitchen.Domain.Entities;

namespace FastTechFoods.Kitchen.Application.ExtensionMethods;
public static class OrderExtensions
{
    public static OrderViewModel ToViewModel(this Order model)
    {
        return new OrderViewModel
        {
            Id = model.Id,
            Status = model.Status,
            CustomerId = model.CustomerId,
            CancellationReason = model.CancellationReason,
            CreatedAt = model.CreatedAt,
            DeliveryMethod = model.DeliveryMethod,
            Items = model.Items.Select(item => new OrderItemViewModel
            {
                Id = item.Id,
                MenuItemId = item.MenuItemId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            }).ToList()
        };
    }

    public static IEnumerable<OrderViewModel> ToViewModel(this IEnumerable<Order> model)
        => model.Select(model => model.ToViewModel());

    public static Order ToModel(this OrderViewModel model)
    {
        return new Order
        {
            Id = model.Id,
            Status = model.Status,
            CustomerId = model.CustomerId,
            CancellationReason = model.CancellationReason,
            CreatedAt = model.CreatedAt,
            DeliveryMethod = model.DeliveryMethod,
            Items = model.Items.Select(item => new OrderItem
            {
                Id = item.Id,
                MenuItemId = item.MenuItemId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            }).ToList()
        };
    }

    public static AcceptOrRejectOrder ToModelEvent(this UpdateStatusOrderViewModel viewModel)
    {
        return new AcceptOrRejectOrder
            (
                viewModel.Id,
                (EnumStatus)viewModel.Status,
                viewModel.CancellationReason
            );
    }
}


