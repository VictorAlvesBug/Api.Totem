using Api.Totem.Application.DTOs.Categories;
using Api.Totem.Application.DTOs.Products;

namespace Api.Totem.Application.DTOs.OrderItems
{
	public class OrderItemDTO
	{
        public string Id { get; set; }
		public CategoryDTO Category { get; set; }
		public string CategoryId { get; set; }
		public ProductDTO MainProduct { get; set; }
		public string MainProductId { get; set; }
		public IEnumerable<ProductDTO> SideProducts { get; set; }
        public IEnumerable<string> SideProductIds { get; set; }
        public decimal Price { get; set; }
    }
}
