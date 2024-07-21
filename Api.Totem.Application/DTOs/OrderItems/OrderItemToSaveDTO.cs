namespace Api.Totem.Application.DTOs.OrderItems
{
	public class OrderItemToSaveDTO
	{
		public string MainProductId { get; set; }
		public IEnumerable<string> SideProductIds { get; set; }
	}
}
