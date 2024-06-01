using Api.Totem.Domain.Enumerators;

namespace Api.Totem.Domain.Entities
{
	public class Category
	{
		public string Id { get; set; }
		public CategoryType CategoryType { get; set; }
		public string Name { get; set; }
		public List<string> ProductIds { get; set; }
		public List<Product> Products { get; set; }
		public ComplementType ComplementType { get; set; }
		public List<SideDishSet>? SideDishSets { get; set; }
		public List<string>? ComboItemCategoryIds { get; set; }
		public List<Category>? ComboItemCategories { get; set; }
		public decimal? ComboAdditionalPrice { get; set; }
	}
}
