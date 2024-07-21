namespace Api.Totem.Domain.Entities
{
	public class OrderItem
	{
		public string Id { get; set; }
		public string MainProductId { get; set; }
		public IEnumerable<string> SideProductIds { get; set; }
		public decimal Price { get; set; }
	}
}
