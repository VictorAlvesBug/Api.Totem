using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Enumerators;
using Api.Totem.Helpers.Extensions;
using AutoFixture;
using AutoFixture.AutoMoq;
using System;

namespace Api.Totem.Application.Test.Helpers
{
	public class DatabaseMockHelper
	{
		/*private IFixture _fixture;
		private Random _random;
		public List<Product> products;
		public List<Category> categories;
		public List<Order> orders;

		public DatabaseMockHelper(IFixture fixture)
		{
			_fixture = fixture;
			_random = new Random();

			InitProducts();
			InitCategories();
			InitOrders();
		}

		private void InitProducts(int amount = 10)
		{
			if (amount <= 0)
				throw new ArgumentException($"The {nameof(amount)} parameter must have a non-zero positive value.");

			products = new List<Product>();

			for (int i = 0; i < amount; i++)
				products.Add(GenerateProduct());
		}

		private void InitCategories(int amount = 50)
		{
			if (amount <= 0)
				throw new ArgumentException($"The {nameof(amount)} parameter must have a non-zero positive value.");

			if(!products.SafeAny())
				throw new Exception($"The {nameof(products)} list must be defined before the categories.");

			var productIds = products.Select(product => product.Id);

			categories = new List<Category>();

			for (int i = 0; i < Math.Floor(amount/2.0); i++)
				categories.Add(GenerateCategory(CategoryType.SubCategory));

			for (int i = 0; i < Math.Ceiling(amount / 2.0); i++)
				categories.Add(GenerateCategory(CategoryType.MainCategory));
		}

		private void InitOrders(int amount = 10)
		{
			if (amount <= 0)
				throw new ArgumentException($"The {nameof(amount)} parameter must have a non-zero positive value.");

			if (!products.SafeAny())
				throw new Exception($"The {nameof(products)} list must be defined before the orders.");

			if (!categories.SafeAny())
				throw new Exception($"The {nameof(categories)} list must be defined before the orders.");

			orders = new List<Order>();

			for (int i = 0; i < amount; i++)
				orders.Add(GenerateOrder());
		}

		private Product GenerateProduct()
		{
			return _fixture
				.Build<Product>()
				.With(product => product.Price, _random.Next(1, 100))
				.With(product => product.Available, true)
				.Create();
		}

		private Category GenerateCategory(CategoryType categoryType)
		{
			switch (categoryType)
			{
				case CategoryType.MainCategory:
					var subCategoryIds = categories
						.Where(category => category.CategoryType == CategoryType.SubCategory)
						.Select(category => category.Id);

					if (!subCategoryIds.SafeAny())
						throw new Exception($"Cannot generate {nameof(CategoryType.MainCategory)} because no {nameof(CategoryType.SubCategory)} was found.");
					
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
								.With(sideDishSet => sideDishSet.Amount, _random.Next(1, 5))
								.With(sideDishSet => sideDishSet.CategoryId, subCategoryIds.PickOneRandomly())
								.CreateMany(10);
							mainCategory.ComboItemCategoryIds = null;
							mainCategory.ComboAdditionalPrice = null;
							break;
					}

					return mainCategory;

				case CategoryType.SubCategory:
					return _fixture
						.Build<Category>()
						.With(category => category.CategoryType, CategoryType.SubCategory)
						.With(category => category.ComplementType, ComplementType.None)
						.With(category => category.ProductIds, products.PickManyRandomly().Select(product => product.Id))
						.Without(category => category.SideDishSets)
						.Without(category => category.ComboItemCategoryIds)
						.Without(category => category.ComboAdditionalPrice)
						.Create();

				default:
					throw new NotImplementedException();
			}
		}

		private Order GenerateOrder(int orderItemsAmount = 5)
		{
			var orderItems = new List<OrderItem>();

			for (int i = 0; i < orderItemsAmount; i++)
			{
				var randomCategory = categories
				.Where(category => category.CategoryType == CategoryType.MainCategory)
				.PickOneRandomly();

				if (randomCategory == null)
					throw new Exception($"Cannot generate {nameof(Order)} because no {nameof(CategoryType.MainCategory)} was found.");

				var randomMainProductId = randomCategory.ProductIds.PickOneRandomly();

				var randomMainProduct = products
					.FirstOrDefault(product => product.Id == randomMainProductId);

				if (randomMainProduct == null)
					throw new Exception($"Cannot generate {nameof(Order)} because no {nameof(Product)} was found with {nameof(Product.Id)} = {randomMainProductId}.");

				var orderItem = _fixture.Build<OrderItem>()
					.With(orderItem => orderItem.CategoryId, randomCategory.Id)
					.With(orderItem => orderItem.MainProductId, randomMainProduct.Id)
					.With(orderItem => orderItem.Price, _random.Next(1, 100))
					.Create();

				switch (randomCategory.ComplementType)
				{
					case ComplementType.None:
						orderItem.SideProductIds = new List<string>();
						break;

					case ComplementType.OptionalCombo:
						if(_random.NextDouble() > 0.5)
							orderItem.SideProductIds = categories
								.Where(category => randomCategory.ComboItemCategoryIds.Contains(category.Id))
								.Select(category => category.ProductIds.PickOneRandomly());
						else
							orderItem.SideProductIds = new List<string>();
						break;

					case ComplementType.SideDishes:
						if(!randomCategory.SideDishSets.SafeAny())
							throw new Exception($"Cannot generate {nameof(Order)} because no {nameof(randomCategory.SideDishSets)} was found.");

						var randomSideDishSet = randomCategory.SideDishSets.PickOneRandomly();

						var randomSideDishSetCategory = categories
							.FirstOrDefault(category => randomSideDishSet?.CategoryId == category.Id);

						if (randomSideDishSetCategory == null)
							throw new Exception($"Cannot generate {nameof(Order)} because no {nameof(Category)} was found for this {nameof(randomCategory.SideDishSets)}.");

						orderItem.SideProductIds = randomSideDishSetCategory.ProductIds.PickManyRandomly(randomSideDishSet.Amount);
						break;

					default:
						throw new NotImplementedException();
				}
			}

			return _fixture
				.Build<Order>()
				.With(order => order.Items, orderItems)
				.Create();
		}*/
	}
}
