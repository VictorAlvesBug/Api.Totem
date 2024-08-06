using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Enumerators;
using Api.Totem.Helpers.Extensions;
using AutoFixture;
using AutoFixture.AutoMoq;

namespace Api.Totem.Application.Test.Helpers
{
	public class DatabaseMockHelper
	{
		//private static DatabaseMockHelper _instance;
        private IFixture _fixture;
		public List<Product> products;
		public List<Category> categories;

        public DatabaseMockHelper(IFixture fixture)
        {
			_fixture = fixture;
			var random = new Random();

			products = _fixture
                .Build<Product>()
				.With(product => product.Price, random.Next(1, 100))
				.With(product => product.Available, true)
                .CreateMany(10)
				.ToList();

            var productIds = products.Select(product => product.Id);

			categories = new List<Category>();

			for (int i = 0; i < 50; i++)
			{
				var subCategory = _fixture
					.Build<Category>()
					.With(category => category.CategoryType, CategoryType.SubCategory)
					.With(category => category.ComplementType, ComplementType.None)
					.With(category => category.ProductIds, products.PickManyRandomly().Select(product => product.Id))
					.Without(category => category.SideDishSets)
					.Without(category => category.ComboItemCategoryIds)
					.Without(category => category.ComboAdditionalPrice)
					.Create();

				categories.Add(subCategory);
			}

			var subCategoryIds = categories.Select(category => category.Id);

			for (int i = 0; i < 50; i++)
			{
				var mainCategory = _fixture
					.Build<Category>()
					.With(category => category.CategoryType, CategoryType.MainCategory)
					.With(category => category.ProductIds, products.PickManyRandomly().Select(product => product.Id))
					.Create();

				switch (mainCategory.ComplementType)
				{
					case ComplementType.None:
						mainCategory.SideDishSets = null;
						mainCategory.ComboItemCategoryIds = null;
						mainCategory.ComboAdditionalPrice = null;
						break;

					case ComplementType.OptionalCombo:
						mainCategory.SideDishSets = null;
						mainCategory.ComboItemCategoryIds = subCategoryIds.PickManyRandomly();
						mainCategory.ComboAdditionalPrice = Math.Abs(_fixture.Create<decimal>());
						break;

					case ComplementType.SideDishes:
						mainCategory.SideDishSets = _fixture
							.Build<SideDishSet>()
							.With(sideDishSet => sideDishSet.Amount, random.Next(1, 5))
							.With(sideDishSet => sideDishSet.CategoryId, subCategoryIds.PickOneRandomly())
							.CreateMany(10);
						mainCategory.ComboItemCategoryIds = null;
						mainCategory.ComboAdditionalPrice = null;
						break;
				}

				categories.Add(mainCategory);
			}
		}
    }
}
