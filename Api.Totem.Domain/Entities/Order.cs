using Api.Totem.Domain.Enumerators;

namespace Api.Totem.Domain.Entities
{
	public class Order : BaseEntity
	{
		public OrderType? Type { get; set; }
		public IEnumerable<OrderItem> Items { get; set; }
		public decimal TotalPrice { get; set; }
		public PaymentMethod? PaymentMethod { get; set; }
		public string Comment { get; set; }
		public OrderStatus Status { get; set; }
		public int PagerId { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? OrderedDate { get; set; }
	}
}
