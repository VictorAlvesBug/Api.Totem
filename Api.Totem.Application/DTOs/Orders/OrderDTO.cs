using Api.Totem.Application.DTOs.OrderItems;
using Api.Totem.Domain.Enumerators;

namespace Api.Totem.Application.DTOs.Orders
{
	public class OrderDTO
	{
		public string Id { get; set; }
		public OrderType? Type { get; set; }
		public IEnumerable<OrderItemDTO> Items { get; set; }
		public decimal TotalPrice { get; set; }
		public PaymentMethod? PaymentMethod { get; set; }
		public string Comment { get; set; }
		public OrderStatus Status { get; set; }
		public int PagerId { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? OrderedDate { get; set; }
	}
}
