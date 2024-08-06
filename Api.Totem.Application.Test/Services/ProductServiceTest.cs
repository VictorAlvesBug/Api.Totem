using Api.Totem.Application.DTOs.Products;
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
	public class ProductServiceTest
	{
		private readonly IFixture _fixture;
		private readonly DatabaseMockHelper _databaseMock;
		private readonly AutoMocker _mocker;
        public ProductServiceTest()
        {
			_fixture = new Fixture().Customize(new AutoMoqCustomization());
			_mocker = new AutoMocker();
			_databaseMock = new DatabaseMockHelper(_fixture);
		}

        [Fact]
		public void ListProductsTest()
		{
			// Arrange
			var productService = CreateProductServiceInstance();

			// Act
			var actualProducts = productService.List().ToList();

			// Assert
			Assert.Equal(_databaseMock.products.Count, actualProducts.Count);
		}

		[Fact]
		public void GetProductTest()
		{
			// Arrange
			var productService = CreateProductServiceInstance();
			var expectedProduct = _databaseMock.products.PickOneRandomly();

			// Act
			var actualProduct = productService.Get(expectedProduct.Id);

			// Assert
			Assert.NotNull(actualProduct);
			Assert.Equal(expectedProduct.Id, actualProduct.Id);
		}

		[Fact]
		public void GetInvalidProductTest()
		{
			// Arrange
			var productService = CreateProductServiceInstance();
			string invalidId = Guid.NewGuid().ToString();

			// Act & Assert
			Assert.Throws<ArgumentException>(() => productService.Get(invalidId));
		}

		[Fact]
		public void CreateProductTest()
		{
			// Arrange
			var productService = CreateProductServiceInstance();
			var expectedProduct = _fixture.Create<ProductToCreateDTO>();

			// Act
			var actualProduct = productService.Create(expectedProduct);

			// Assert
			Assert.NotNull(actualProduct);
			Assert.Equal(expectedProduct.Name, actualProduct.Name);
			Assert.Equal(expectedProduct.Description, actualProduct.Description);
			Assert.Equal(expectedProduct.Price, actualProduct.Price);
			Assert.NotNull(actualProduct.Id);
			Assert.True(actualProduct.Available);
		}

		[Fact]
		public void CreateInvalidProductTest()
		{
			// Arrange
			var productService = CreateProductServiceInstance();

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => productService.Create(null));
		}

		[Fact]
		public void UpdateProductTest()
		{
			// Arrange
			var productService = CreateProductServiceInstance();
			var id = _databaseMock.products.PickOneRandomly().Id;
			var expectedProduct = _fixture.Create<ProductToUpdateDTO>();

			// Act
			var actualProduct = productService.Update(id, expectedProduct);

			// Assert
			Assert.NotNull(actualProduct);
			Assert.Equal(id, actualProduct.Id);
			Assert.Equal(expectedProduct.Name, actualProduct.Name);
			Assert.Equal(expectedProduct.Description, actualProduct.Description);
			Assert.Equal(expectedProduct.Price, actualProduct.Price);
		}

		[Fact]
		public void UpdateInvalidProductTest()
		{
			// Arrange
			var productService = CreateProductServiceInstance();
			var id = _databaseMock.products.PickOneRandomly().Id;

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => productService.Update(id, null));
		}

		[Fact]
		public void UpdateProductWithInvalidIdTest()
		{
			// Arrange
			var productService = CreateProductServiceInstance();
			string invalidId = Guid.NewGuid().ToString();

			// Act & Assert
			Assert.Throws<ArgumentException>(() => productService.Update(invalidId, _fixture.Create<ProductToUpdateDTO>()));
		}

		[Fact]
		public void UpdateInvalidProductWithInvalidIdTest()
		{
			// Arrange
			var productService = CreateProductServiceInstance();
			string invalidId = Guid.NewGuid().ToString();

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => productService.Update(invalidId, null));
		}

		[Fact]
		public void UpdateProductAvailabilityTest()
		{
			// Arrange
			var productService = CreateProductServiceInstance();
			var id = _databaseMock.products.PickOneRandomly().Id;
			var expectedProduct = _fixture.Create<ProductToUpdateAvailabilityDTO>();

			// Act
			var actualProduct = productService.UpdateAvailability(id, expectedProduct);

			// Assert
			Assert.NotNull(actualProduct);
			Assert.Equal(expectedProduct.Available, actualProduct.Available);
		}

		[Fact]
		public void UpdateInvalidProductAvailabilityTest()
		{
			// Arrange
			var productService = CreateProductServiceInstance();
			var id = _databaseMock.products.PickOneRandomly().Id;

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => productService.UpdateAvailability(id, null));
		}

		[Fact]
		public void UpdateProductAvailabilityWithInvalidIdTest()
		{
			// Arrange
			var productService = CreateProductServiceInstance();
			string invalidId = Guid.NewGuid().ToString();

			// Act & Assert
			Assert.Throws<ArgumentException>(() => productService.UpdateAvailability(invalidId, _fixture.Create<ProductToUpdateAvailabilityDTO>()));
		}

		[Fact]
		public void UpdateInvalidProductAvailabilityWithInvalidIdTest()
		{
			// Arrange
			var productService = CreateProductServiceInstance();
			string invalidId = Guid.NewGuid().ToString();

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => productService.UpdateAvailability(invalidId, null));
		}

		[Fact]
		public void DeleteProductTest()
		{
			// Arrange
			var productService = CreateProductServiceInstance();
			var id = _databaseMock.products.PickOneRandomly().Id;

			// Act & Assert
			productService.Delete(id);
		}

		[Fact]
		public void DeleteInvalidProductTest()
		{
			// Arrange
			var productService = CreateProductServiceInstance();
			string invalidId = Guid.NewGuid().ToString();

			// Act & Assert
			Assert.Throws<ArgumentException>(() => productService.Delete(invalidId));
		}

		private IProductService CreateProductServiceInstance()
		{
			var products = new List<Product>(_databaseMock.products);
			RepositoryMockHelper.SetupMockRepository<IProductRepository, Product>(_mocker, products);
			return _mocker.CreateInstance<ProductService>();
		}
	}
}
