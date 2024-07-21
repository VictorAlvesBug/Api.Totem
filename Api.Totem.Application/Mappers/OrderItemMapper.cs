using Api.Totem.Application.DTOs.OrderItems;
using Api.Totem.Domain.Entities;
using Api.Totem.Helpers.Extensions;

namespace Api.Totem.Application.Mappers
{
	public static class OrderItemMapper
	{
		public static OrderItem MapToOrderItem(this OrderItemToSaveDTO orderItemToSaveDTO)
		{
			return orderItemToSaveDTO.ConvertTo<OrderItem>();
		}

		public static IEnumerable<OrderItem> MapToOrderItem(this IEnumerable<OrderItemToSaveDTO> orderItemsToSaveDTO)
		{
			return orderItemsToSaveDTO.Select(orderItemToSaveDTO => MapToOrderItem(orderItemToSaveDTO));
		}
	}
}
