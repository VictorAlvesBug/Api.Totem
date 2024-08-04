using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Interfaces.Repositories;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using Moq.AutoMock;

namespace Api.Totem.Application.Test.Helpers
{
	public class RepositoryMockHelper
	{
		public static Mock<TRepository> SetupMockRepository<TRepository, TEntity>(AutoMocker mocker, List<TEntity> entities)
			where TRepository : class, IBaseRepository<TEntity>
			where TEntity : BaseEntity
		{
			IFixture fixture = new Fixture().Customize(new AutoMoqCustomization());

			string entityName = typeof(TEntity).Name;

			var mockRepository = mocker.GetMock<TRepository>();

			mockRepository
				.Setup(repo => repo.List())
				.Returns(entities);

			TEntity outValue = default;
			mockRepository
				.Setup(repo => repo.TryGet(It.IsAny<string>(), out outValue))
				.Returns((string id, out TEntity entity) =>
				{
					entity = entities.Find(e => e.Id == id);
					return entity != null;
				});

			mockRepository
				.Setup(repo => repo.Get(It.IsAny<string>()))
				.Returns((string id) =>
				{
					var entity = entities.Find(e => e.Id == id);

					if (entity == null)
						throw new ArgumentException($"No {entityName} was found with {nameof(BaseEntity.Id)} = {id}.");

					return entity;
				});

			mockRepository
				.Setup(repo => repo.Create(It.IsAny<TEntity>()))
				.Returns((TEntity entity) =>
				{
					entities.Add(entity);

					return entity;
				});

			mockRepository
				.Setup(repo => repo.Update(It.IsAny<TEntity>()))
				.Returns((TEntity entity) =>
				{
					var index = entities.FindIndex(e => e.Id == entity.Id);

					if (index == -1)
						throw new ArgumentException($"No {entityName} was found with {nameof(BaseEntity.Id)} = {entity.Id}.");
					else
						entities[index] = entity;

					return entity;
				});

			mockRepository
				.Setup(repo => repo.Delete(It.IsAny<string>()))
				.Callback((string id) => {
					var entity = entities.Find(e => e.Id == id);

					if (entity == null)
						throw new ArgumentException($"No {entityName} was found with {nameof(BaseEntity.Id)} = {id}.");

					entities.Remove(entity);
				});

			return mockRepository;
		}

	}
}
