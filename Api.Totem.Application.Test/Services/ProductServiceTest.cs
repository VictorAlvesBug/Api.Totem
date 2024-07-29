using Api.Totem.Application.DTOs.Products;
using Api.Totem.Application.Services;
using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Interfaces.Repositories;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;

namespace Api.Totem.Application.Test.Services
{
	public class ProductServiceTest
	{
		private readonly IFixture _fixture;
		private readonly List<Product> _mockProducts;
		private readonly string _mockInvalidId;
        public ProductServiceTest()
        {
			_fixture = new Fixture().Customize(new AutoMoqCustomization());

			_mockProducts = new List<Product>
			{
				_fixture.Create<Product>(),
				_fixture.Create<Product>(),
				_fixture.Create<Product>(),
				_fixture.Create<Product>(),
				_fixture.Create<Product>(),
			};

			_mockInvalidId = "INVALID_ID";
		}

        [Fact]
		public void ListProductsTest()
		{
			// Arrange
			var productService = new ProductService(CreateProductRepositoryMock().Object);

			// Act
			var actual = productService.List().ToList();

			// Assert
			Assert.Equal(_mockProducts.Count, actual.Count);
		}

		[Fact]
		public void GetProductTest()
		{
			// Arrange
			var productService = new ProductService(CreateProductRepositoryMock().Object);

			// Act
			var actual = productService.Get(_fixture.Create<string>());

			// Assert
			Assert.NotNull(actual);
		}

		[Fact]
		public void GetInvalidProductTest()
		{
			// Arrange
			var productService = new ProductService(CreateProductRepositoryMock().Object);

			// Act & Assert
			Assert.Throws<ArgumentException>(() => productService.Get(_mockInvalidId));
		}

		[Fact]
		public void CreateProductTest()
		{
			// Arrange
			var productService = new ProductService(CreateProductRepositoryMock().Object);

			// Act
			var actual = productService.Create(_fixture.Create<ProductToCreateDTO>());

			// Assert
			Assert.NotNull(actual.Id);
			Assert.True(actual.Available);
		}

		[Fact]
		public void CreateInvalidProductTest()
		{
			// Arrange
			var productService = new ProductService(CreateProductRepositoryMock().Object);

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => productService.Create(null));
		}

		[Fact]
		public void UpdateProductTest()
		{
			// Arrange
			var productService = new ProductService(CreateProductRepositoryMock().Object);
			var productToUpdateDTO = _fixture.Create<ProductToUpdateDTO>();

			// Act
			var actual = productService.Update(_fixture.Create<string>(), productToUpdateDTO);

			// Assert
			Assert.Equal(productToUpdateDTO.Name, actual.Name);
			Assert.Equal(productToUpdateDTO.Description, actual.Description);
			Assert.Equal(productToUpdateDTO.Price, actual.Price);
		}

		[Fact]
		public void UpdateInvalidProductTest()
		{
			// Arrange
			var productService = new ProductService(CreateProductRepositoryMock().Object);

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => productService.Update(_fixture.Create<string>(), null));
		}

		[Fact]
		public void UpdateProductWithInvalidIdTest()
		{
			// Arrange
			var productService = new ProductService(CreateProductRepositoryMock().Object);

			// Act & Assert
			Assert.Throws<ArgumentException>(() => productService.Update(_mockInvalidId, _fixture.Create<ProductToUpdateDTO>()));
		}

		[Fact]
		public void UpdateInvalidProductWithInvalidIdTest()
		{
			// Arrange
			var productService = new ProductService(CreateProductRepositoryMock().Object);

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => productService.Update(_mockInvalidId, null));
		}

		[Fact]
		public void UpdateProductAvailabilityTest()
		{
			// Arrange
			var productService = new ProductService(CreateProductRepositoryMock().Object);
			var productToUpdateAvailabilityDTO = _fixture.Create<ProductToUpdateAvailabilityDTO>();

			// Act
			var actual = productService.UpdateAvailability(_fixture.Create<string>(), productToUpdateAvailabilityDTO);

			// Assert
			Assert.Equal(productToUpdateAvailabilityDTO.Available, actual.Available);
		}

		[Fact]
		public void UpdateInvalidProductAvailabilityTest()
		{
			// Arrange
			var productService = new ProductService(CreateProductRepositoryMock().Object);

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => productService.UpdateAvailability(_fixture.Create<string>(), null));
		}

		[Fact]
		public void UpdateProductAvailabilityWithInvalidIdTest()
		{
			// Arrange
			var productService = new ProductService(CreateProductRepositoryMock().Object);

			// Act & Assert
			Assert.Throws<ArgumentException>(() => productService.UpdateAvailability(_mockInvalidId, _fixture.Create<ProductToUpdateAvailabilityDTO>()));
		}

		[Fact]
		public void UpdateInvalidProductAvailabilityWithInvalidIdTest()
		{
			// Arrange
			var productService = new ProductService(CreateProductRepositoryMock().Object);

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => productService.UpdateAvailability(_mockInvalidId, null));
		}

		[Fact]
		public void DeleteProductTest()
		{
			// Arrange
			var productService = new ProductService(CreateProductRepositoryMock().Object);

			// Act & Assert
			productService.Delete(_fixture.Create<string>());
		}

		[Fact]
		public void DeleteInvalidProductTest()
		{
			// Arrange
			var productService = new ProductService(CreateProductRepositoryMock().Object);

			// Act & Assert
			Assert.Throws<ArgumentException>(() => productService.Delete(_mockInvalidId));
		}

		private Mock<IProductRepository> CreateProductRepositoryMock()
		{
			var productRepository = new Mock<IProductRepository>();

			productRepository
				.Setup(repo => repo.List())
				.Returns(_mockProducts);

			Product outValidValue = new Product();
			productRepository
				.Setup(repo => repo.TryGet(It.IsAny<string>(), out outValidValue))
				.Returns(true);

			Product outInvalidValue;
			productRepository
				.Setup(repo => repo.TryGet(_mockInvalidId, out outInvalidValue))
				.Returns(false);

			productRepository
				.Setup(repo => repo.Get(It.IsAny<string>()))
				.Returns((string id) =>
					_fixture
						.Build<Product>()
						.With(x => x.Id, id)
						.Create());

			productRepository
				.Setup(repo => repo.Get(_mockInvalidId))
				.Throws<ArgumentException>();

			productRepository
				.Setup(repo => repo.Create(It.IsAny<Product>()))
				.Returns((Product product) => product);

			productRepository
				.Setup(repo => repo.Update(It.IsAny<Product>()))
				.Returns((Product product) => product);

			productRepository
				.Setup(repo => repo.Delete(It.IsAny<string>()));

			productRepository
				.Setup(repo => repo.Delete(_mockInvalidId))
				.Throws<ArgumentException>();

			return productRepository;
		}
	}
}
