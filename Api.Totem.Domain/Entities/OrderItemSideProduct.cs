namespace Api.Totem.Domain.Entities
{
	public class OrderItemSideProduct : BaseEntity
	{
		public string OrderItemId { get; set; }
		public string SideProductId { get; set; }
	}
}
