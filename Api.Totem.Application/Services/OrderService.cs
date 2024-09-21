using Api.Totem.Application.DTOs.Categories;
using Api.Totem.Application.DTOs.OrderItems;
using Api.Totem.Application.DTOs.Orders;
using Api.Totem.Application.DTOs.Products;
using Api.Totem.Application.Interfaces;
using Api.Totem.Application.Mappers;
using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Enumerators;
using Api.Totem.Domain.Interfaces.Repositories;
using Api.Totem.Helpers.Extensions;
using System.Net.Http.Headers;
using System.Text;

namespace Api.Totem.Application.Services
{
	public class OrderService : IOrderService
	{
		private readonly IBaseRepository<Order> _orderRepository;
		private readonly IBaseRepository<Product> _productRepository;
		private readonly IBaseRepository<Category> _categoryRepository;

		public OrderService(
			IBaseRepository<Order> orderRepository,
			IBaseRepository<Product> productRepository,
			IBaseRepository<Category> categoryRepository)
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

		public OrderToShowDTO SetType(string id, DeliveryType orderType)
		{
			var order = _orderRepository.Get(id);

			order.DeliveryType = orderType;

			return PrepareToShow(Save(order));
		}

		public OrderToShowDTO SetPaymentMethod(string id, PaymentMethod paymentMethod)
		{
			var order = _orderRepository.Get(id);

			order.PaymentMethod = paymentMethod;

			return PrepareToShow(Save(order));
		}

		public OrderToShowDTO SetPagerId(string id, int pagerId)
		{
			var order = _orderRepository.Get(id);

			order.PagerId = pagerId;

			return PrepareToShow(Save(order));
		}

		public OrderToShowDTO SetComment(string id, string comment)
		{
			var order = _orderRepository.Get(id);

			order.Comment = comment;

			return PrepareToShow(Save(order));
		}

		public OrderToShowDTO Confirm(string id)
		{
			var order = _orderRepository.Get(id);

			Validate(order);
			order.Status = OrderStatus.Ordered;
			order.OrderedDate = DateTime.Now;

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

            foreach (var sideProductId in orderItem.SideProductIds)
            {
				orderItem.Price += _productRepository.Get(sideProductId).Price;
			}
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
			foreach (var orderItem in orderDTO.Items)
			{
				orderItem.Category = _categoryRepository.Get(orderItem.CategoryId).MapToCategoryDTO();

				orderItem.MainProduct = _productRepository.Get(orderItem.MainProductId).MapToProductDTO();

				orderItem.SideProducts = orderItem.SideProductIds
					.Select(sideProductId => _productRepository.Get(sideProductId).MapToProductDTO());
			}
		}

		private void FillAdditionalPropertiesToShow(IEnumerable<OrderDTO> ordersDTO)
		{
            foreach (var orderDTO in ordersDTO)
            {
				FillAdditionalPropertiesToShow(orderDTO);
			}
		}

		private void Validate(Order order)
		{
			if (order == null)
				throw new Exception("The order cannot be null.");

			if (!order.Items.SafeAny())
				throw new Exception("The order must have at least one item.");

			var tuplesIdsNotFound = new List<Tuple<string, string>>();

			foreach (var item in order.Items)
			{
				if (!_categoryRepository.TryGet(item.CategoryId, out _))
					tuplesIdsNotFound.Add(new Tuple<string, string>(nameof(Category), item.CategoryId));

				if (!_productRepository.TryGet(item.MainProductId, out _))
					tuplesIdsNotFound.Add(new Tuple<string, string>(nameof(Product), item.MainProductId));

                foreach (var sideProductId in item.SideProductIds)
					if (!_productRepository.TryGet(sideProductId, out _))
						tuplesIdsNotFound.Add(new Tuple<string, string>(nameof(Product), sideProductId));
			}

			if (tuplesIdsNotFound.SafeAny())
			{
				var sb = new StringBuilder();

				foreach (var group in tuplesIdsNotFound.GroupBy(tuple => tuple.Item1))
				{
					var entityName = group.Key.ToCamelCase();
					var idsNotFound = group.Select(item => item.Item2);

					sb.AppendLine($"No {entityName} were found with the following Id(s): {idsNotFound.JoinThis()}.");
				}

				throw new ArgumentException(sb.ToString());
			}
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
