using Api.Totem.Application.DTOs.Products;

namespace Api.Totem.Application.DTOs.OrderItems
{
	public class OrderItemToShowDTO
	{
        public string Id { get; set; }
        public ProductToShowDTO MainProduct { get; set; }
		public IEnumerable<ProductToShowDTO> SideProducts { get; set; }
        public decimal Price { get; set; }
    }
}
