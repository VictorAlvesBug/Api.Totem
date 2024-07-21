using Api.Totem.Application.DTOs.Orders;
using Api.Totem.Domain.Entities;
using Api.Totem.Helpers.Extensions;

namespace Api.Totem.Application.Mappers
{
	public static class OrderMapper
	{
		public static OrderDTO MapToOrderDTO(this Order order)
		{
			return order.ConvertTo<OrderDTO>();
		}

		public static IEnumerable<OrderDTO> MapToOrderDTO(this IEnumerable<Order> orders)
		{
			return orders.Select(order => MapToOrderDTO(order));
		}

		public static OrderToShowDTO MapToOrderToShowDTO(this OrderDTO orderDTO)
		{
			return orderDTO.ConvertTo<OrderToShowDTO>();
		}

		public static IEnumerable<OrderToShowDTO> MapToOrderToShowDTO(this IEnumerable<OrderDTO> ordersDTO)
		{
			return ordersDTO.Select(orderDTO => MapToOrderToShowDTO(orderDTO));
		}

		public static OrderToShowDTO MapToOrderToShowDTO(this Order order)
		{
			return order.ConvertTo<OrderToShowDTO>();
		}

		public static IEnumerable<OrderToShowDTO> MapToOrderToShowDTO(this IEnumerable<Order> orders)
		{
			return orders.Select(order => MapToOrderToShowDTO(order));
		}

		public static Order MapToOrder(this OrderDTO orderDTO)
		{
			return orderDTO.ConvertTo<Order>();
		}

		public static IEnumerable<Order> MapToOrder(this IEnumerable<OrderDTO> ordersDTO)
		{
			return ordersDTO.Select(orderDTO => MapToOrder(orderDTO));
		}

		public static Order MapToOrder(this OrderToSetTypeDTO orderToSetTypeDTO)
		{
			return orderToSetTypeDTO.ConvertTo<Order>();
		}

		public static IEnumerable<Order> MapToOrder(this IEnumerable<OrderToSetTypeDTO> ordersToSetTypeDTO)
		{
			return ordersToSetTypeDTO.Select(orderToSetTypeDTO => MapToOrder(orderToSetTypeDTO));
		}
	}
}
