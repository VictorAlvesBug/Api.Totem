using Api.Totem.Application.DTOs.Categories;
using Api.Totem.Application.DTOs.OrderItems;
using Api.Totem.Application.DTOs.Orders;
using Api.Totem.Application.DTOs.Products;
using Api.Totem.Application.Interfaces;
using Api.Totem.Application.Mappers;
using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Enumerators;
using Api.Totem.Domain.Interfaces.Repositories;

namespace Api.Totem.Application.Services
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IProductRepository _productRepository;
		private readonly ICategoryRepository _categoryRepository;

		public OrderService(
			IOrderRepository orderRepository,
			IProductRepository productRepository,
			ICategoryRepository categoryRepository)
		{
			_orderRepository = orderRepository;
			_productRepository = productRepository;
			_categoryRepository = categoryRepository;
		}

		public IEnumerable<OrderToShowDTO> List()
		{
			return PrepareToShow(_orderRepository.List());
		}

		public OrderToShowDTO Get(string id)
		{
			return PrepareToShow(_orderRepository.Get(id));
		}

		public OrderToShowDTO Create()
		{
			var order = new Order
			{
				Id = Guid.NewGuid().ToString(),
				CreatedDate = DateTime.Now,
				Items = new List<OrderItem>(),
				Status = OrderStatus.Creating,
				TotalPrice = 0,
				Comment = string.Empty
			};

			return PrepareToShow(Save(order));
		}

		public OrderToShowDTO SetType(string id, OrderToSetTypeDTO orderToSetTypeDTO)
		{
			var order = _orderRepository.Get(id);

			order.Type = orderToSetTypeDTO.Type;

			return PrepareToShow(Save(order));
		}

		public OrderToShowDTO AddItem(string id, OrderItemToSaveDTO orderItemToSaveDTO)
		{
			var order = _orderRepository.Get(id);

			var orderItem = orderItemToSaveDTO.MapToOrderItem();
			orderItem.Id = Guid.NewGuid().ToString();
			UpdateOrderItemPrice(orderItem);

			order.AddItem(orderItem);

			return PrepareToShow(Save(order));
		}

		public OrderToShowDTO UpdateItem(string id, string orderItemId, OrderItemToSaveDTO orderItemToSaveDTO)
		{
			var order = _orderRepository.Get(id);

			var orderItem = orderItemToSaveDTO.MapToOrderItem();
			orderItem.Id = orderItemId;
			UpdateOrderItemPrice(orderItem);

			order.UpdateItem(orderItem);

			return PrepareToShow(Save(order));
		}

		public OrderToShowDTO RemoveItem(string id, string orderItemId)
		{
			var order = _orderRepository.Get(id);

			order.RemoveItem(orderItemId);

			return PrepareToShow(Save(order));
		}

		public void Delete(string id)
		{
			_orderRepository.Delete(id);
		}

		private void UpdateOrderItemPrice(OrderItem orderItem)
		{
			orderItem.Price = 0;

			orderItem.Price += _productRepository.Get(orderItem.MainProductId).Price;

			orderItem.SideProductIds.ToList().ForEach(sideProductId =>
			{
				orderItem.Price += _productRepository.Get(sideProductId).Price;
			});
		}

		private Order Save(Order order)
		{
			if (string.IsNullOrEmpty(order.Id))
				return _orderRepository.Create(order);

			return _orderRepository.Update(order);
		}

		private OrderToShowDTO PrepareToShow(Order order)
		{
			var orderDTO = order.MapToOrderDTO();
			FillAdditionalPropertiesToShow(orderDTO);
			return orderDTO.MapToOrderToShowDTO();
		}

		private IEnumerable<OrderToShowDTO> PrepareToShow(IEnumerable<Order> orders)
		{
			var ordersDTO = orders.MapToOrderDTO();
			FillAdditionalPropertiesToShow(ordersDTO);
			return ordersDTO.MapToOrderToShowDTO();
		}

		private void FillAdditionalPropertiesToShow(OrderDTO orderDTO)
		{
			foreach(var orderItem in orderDTO.Items)
			{
				orderItem.Category = _categoryRepository.Get(orderItem.CategoryId).MapToCategoryDTO();

				orderItem.MainProduct = _productRepository.Get(orderItem.MainProductId).MapToProductDTO();

				orderItem.SideProducts = orderItem.SideProductIds
					.Select(sideProductId => _productRepository.Get(sideProductId).MapToProductDTO());
			}
		}

		private void FillAdditionalPropertiesToShow(IEnumerable<OrderDTO> ordersDTO)
		{
			ordersDTO.ToList().ForEach(orderDTO => FillAdditionalPropertiesToShow(orderDTO));
		}
	}

	public static class OrderExtensions
	{
		public static void AddItem(this Order order, OrderItem orderItem, bool updateTotalPrice = true)
		{
			order.Items = order.Items.Append(orderItem);
			if (updateTotalPrice) order.UpdateTotalPrice();
		}
		public static void UpdateItem(this Order order, OrderItem orderItem)
		{
			order.RemoveItem(orderItem.Id, updateTotalPrice: false);
			order.AddItem(orderItem, updateTotalPrice: false);
			order.UpdateTotalPrice();
		}

		public static void RemoveItem(this Order order, string orderItemId, bool updateTotalPrice = true)
		{
			order.Items = order.Items.Where(item => item.Id != orderItemId);
			if (updateTotalPrice) order.UpdateTotalPrice();
		}

		public static void UpdateTotalPrice(this Order order)
		{
			order.TotalPrice = order.Items.Sum(item => item.Price);
		}
	}
}
