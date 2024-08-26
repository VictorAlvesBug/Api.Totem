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
	public class OrderServiceTest
	{
		private readonly IFixture _fixture;
		private readonly DatabaseMockHelper _databaseMock;
		private readonly AutoMocker _mocker;
        public OrderServiceTest()
        {
			_fixture = new Fixture().Customize(new AutoMoqCustomization());
			_mocker = new AutoMocker();
			_databaseMock = new DatabaseMockHelper(_fixture);
		}

		private IOrderService CreateOrderServiceInstance()
		{
			var products = new List<Product>(_databaseMock.products);
			var categories = new List<Category>(_databaseMock.categories);
			var orders = new List<Order>(_databaseMock.orders);
			RepositoryMockHelper.SetupMockRepository<IProductRepository, Product>(_mocker, products);
			RepositoryMockHelper.SetupMockRepository<ICategoryRepository, Category>(_mocker, categories);
			RepositoryMockHelper.SetupMockRepository<IOrderRepository, Order>(_mocker, orders);
			return _mocker.CreateInstance<OrderService>();
		}
	}
}
