using Api.Totem.Application.DTOs.Categories;
using Api.Totem.Application.DTOs.SideDishSets;
using Api.Totem.Application.Interfaces;
using Api.Totem.Application.Services;
using Api.Totem.Application.Test.Helpers;
using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Interfaces.Repositories;
using Api.Totem.Helpers.Extensions;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq.AutoMock;

namespace Api.Totem.Application.Test.Services
{
	public class CategoryServiceTest
	{
		/*private readonly IFixture _fixture;
		private readonly DatabaseMockHelper _databaseMock;
		private readonly AutoMocker _mocker;
        public CategoryServiceTest()
		{
			_fixture = new Fixture().Customize(new AutoMoqCustomization());
			_mocker = new AutoMocker();
			_databaseMock = new DatabaseMockHelper(_fixture);
		}

        [Fact]
		public void ListCategoriesTest()
		{
			// Arrange
			var categoryService = CreateCategoryServiceInstance();

			// Act
			var actualCategories = categoryService.List().ToList();

			// Assert
			Assert.Equal(_databaseMock.categories.Count, actualCategories.Count);
		}

		[Fact]
		public void GetCategoryTest()
		{
			// Arrange
			var categoryService = CreateCategoryServiceInstance();
			var expectedCategory = _databaseMock.categories.PickOneRandomly();

			// Act
			var actualCategory = categoryService.Get(expectedCategory.Id);

			// Assert
			Assert.NotNull(actualCategory);
			Assert.Equal(expectedCategory.Id, actualCategory.Id);
		}

		[Fact]
		public void GetInvalidCategoryTest()
		{
			// Arrange
			var categoryService = CreateCategoryServiceInstance();
			string invalidId = Guid.NewGuid().ToString();

			// Act & Assert
			Assert.Throws<ArgumentException>(() => categoryService.Get(invalidId));
		}

		[Fact]
		public void CreateCategoryTest()
		{
			// Arrange
			var categoryService = CreateCategoryServiceInstance();
			var expectedCategory = _fixture
				.Build<CategoryToCreateDTO>()
				.With(c => c.ComboItemCategoryIds, _databaseMock.categories.Select(a => a.Id))
				.With(
					c => c.SideDishSets, 
					_fixture.Build<SideDishSetToCreateDTO>()
						.With(
							sideDishSet => sideDishSet.CategoryId, 
							_databaseMock.categories.PickOneRandomly().Id
						)
						.CreateMany()
				)
				.Create();

			// Act
			var actualCategory = categoryService.Create(expectedCategory);

			// Assert
			Assert.NotNull(actualCategory);
			Assert.NotNull(actualCategory.Id);
			Assert.Equal(expectedCategory.Name, actualCategory.Name);
			Assert.Equal(expectedCategory.CategoryType.ToString(), actualCategory.CategoryType);
			Assert.Equal(expectedCategory.ComplementType.ToString(), actualCategory.ComplementType);
			Assert.Equal(expectedCategory.SideDishSets?.Count(), actualCategory.SideDishSets?.Count());
			Assert.Equal(expectedCategory.ComboAdditionalPrice, actualCategory.ComboAdditionalPrice);
			Assert.Equal(expectedCategory.ComboItemCategoryIds?.Count(), actualCategory.ComboItemCategories?.Count());
		}

		[Fact]
		public void CreateInvalidCategoryTest()
		{
			// Arrange
			var categoryService = CreateCategoryServiceInstance();

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => categoryService.Create(null));
		}

		[Fact]
		public void UpdateCategoryTest()
		{
			// Arrange
			var categoryService = CreateCategoryServiceInstance();
			var id = _databaseMock.categories.PickOneRandomly().Id;
			var expectedCategory = _fixture
				.Build<CategoryToUpdateDTO>()
				.With(c => c.ComboItemCategoryIds, _databaseMock.categories.Select(a => a.Id))
				.With(
					c => c.SideDishSets,
					_fixture.Build<SideDishSetToCreateDTO>()
						.With(
							sideDishSet => sideDishSet.CategoryId,
							_databaseMock.categories.PickOneRandomly().Id
						)
						.CreateMany()
				)
				.Create();

			// Act
			var actualCategory = categoryService.Update(id, expectedCategory);

			// Assert
			Assert.NotNull(actualCategory);
			Assert.Equal(id, actualCategory.Id);
			Assert.Equal(expectedCategory.Name, actualCategory.Name);
			Assert.Equal(expectedCategory.CategoryType.ToString(), actualCategory.CategoryType);
			Assert.Equal(expectedCategory.ComplementType.ToString(), actualCategory.ComplementType);
			Assert.Equal(expectedCategory.SideDishSets?.Count(), actualCategory.SideDishSets?.Count());
			Assert.Equal(expectedCategory.ComboAdditionalPrice, actualCategory.ComboAdditionalPrice);
			Assert.Equal(expectedCategory.ComboItemCategoryIds?.Count(), actualCategory.ComboItemCategories?.Count());
		}

		[Fact]
		public void UpdateInvalidCategoryTest()
		{
			// Arrange
			var categoryService = CreateCategoryServiceInstance();
			var id = _databaseMock.categories.PickOneRandomly().Id;

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => categoryService.Update(id, null));
		}

		[Fact]
		public void UpdateCategoryWithInvalidIdTest()
		{
			// Arrange
			var categoryService = CreateCategoryServiceInstance();
			string invalidId = Guid.NewGuid().ToString();
			var expectedCategory = _fixture
				.Build<CategoryToUpdateDTO>()
				.With(c => c.ComboItemCategoryIds, _databaseMock.categories.Select(a => a.Id))
				.Create();

			// Act & Assert
			Assert.Throws<ArgumentException>(() => categoryService.Update(invalidId, expectedCategory));
		}

		[Fact]
		public void UpdateInvalidCategoryWithInvalidIdTest()
		{
			// Arrange
			var categoryService = CreateCategoryServiceInstance();
			string invalidId = Guid.NewGuid().ToString();

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => categoryService.Update(invalidId, null));
		}

		[Fact]
		public void DeleteCategoryTest()
		{
			// Arrange
			var categoryService = CreateCategoryServiceInstance();
			var id = _databaseMock.categories.PickOneRandomly().Id;

			// Act & Assert
			categoryService.Delete(id);
		}

		[Fact]
		public void DeleteInvalidCategoryTest()
		{
			// Arrange
			var categoryService = CreateCategoryServiceInstance();
			string invalidId = Guid.NewGuid().ToString();

			// Act & Assert
			Assert.Throws<ArgumentException>(() => categoryService.Delete(invalidId));
		}

		[Fact]
		public void AddProductsToCategoryTest()
		{
			// Arrange
			var categoryService = CreateCategoryServiceInstance();
			var category = _databaseMock.categories.PickOneRandomly();
			var categoryProductsToAddDTO = new CategoryProductsToAddDTO
			{
				ProductIds = _databaseMock.products.PickManyRandomly().Select(product => product.Id)
			};
			var expectedProductIds = category.ProductIds.Concat(categoryProductsToAddDTO.ProductIds).Distinct();

			// Act
			var actualCategory = categoryService.AddProducts(category.Id, categoryProductsToAddDTO);

			// Assert
			Assert.NotNull(actualCategory);
			Assert.Equal(category.Id, actualCategory.Id);
			Assert.True(expectedProductIds.HasSameItems(actualCategory.Products.Select(product => product.Id)));
		}

		// TODO: Adicionar testes para cenários de erro do AddProducts

		[Fact]
		public void RemoveProductsFromCategoryTest()
		{
			// Arrange
			var categoryService = CreateCategoryServiceInstance();
			var category = _databaseMock.categories.PickOneRandomly();
			var categoryProductsToRemoveDTO = new CategoryProductsToRemoveDTO
			{
				ProductIds = _databaseMock.products.PickManyRandomly().Select(product => product.Id)
			};
			var expectedProductIds = category.ProductIds.Except(categoryProductsToRemoveDTO.ProductIds);

			// Act
			var actualCategory = categoryService.RemoveProducts(category.Id, categoryProductsToRemoveDTO);

			// Assert
			Assert.NotNull(actualCategory);
			Assert.Equal(category.Id, actualCategory.Id);
			Assert.True(expectedProductIds.HasSameItems(actualCategory.Products.Select(product => product.Id)));
		}

		// TODO: Adicionar testes para cenários de erro do RemoveProducts

		private ICategoryService CreateCategoryServiceInstance()
		{
			var products = new List<Product>(_databaseMock.products);
			var categories = new List<Category>(_databaseMock.categories);
			RepositoryMockHelper.SetupMockRepository<IProductRepository, Product>(_mocker, products);
			RepositoryMockHelper.SetupMockRepository<ICategoryRepository, Category>(_mocker, categories);
			return _mocker.CreateInstance<CategoryService>();
		}*/
	}
}
