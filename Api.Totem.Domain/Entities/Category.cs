using Api.Totem.Domain.Enumerators;

namespace Api.Totem.Domain.Entities
{
	public class Category : BaseEntity
	{
		public CategoryType CategoryType { get; set; }
		public string Name { get; set; }
		//public IEnumerable<string> ProductIds { get; set; }
		public ComplementType ComplementType { get; set; }
		//public IEnumerable<SideDishSet>? SideDishSets { get; set; }
		//public IEnumerable<string>? ComboItemCategoryIds { get; set; }
		public decimal? ComboAdditionalPrice { get; set; }
	}
}
