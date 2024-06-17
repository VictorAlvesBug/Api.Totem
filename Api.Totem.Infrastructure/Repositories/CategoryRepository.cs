using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Interfaces.Repositories;
using Api.Totem.Infrastructure.Utils;

namespace Api.Totem.Infrastructure.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		public List<Category> List()
		{
			return FileUtils.GetListFromFile<Category>();
		}

		public Category Get(string id)
		{
			var categories = FileUtils.GetListFromFile<Category>();

			return categories.FirstOrDefault(p => p.Id == id)
				?? throw new ArgumentException($"No category was found with {nameof(id)} = {id}.");
		}

		public Category Create(Category category)
		{
			var categories = FileUtils.GetListFromFile<Category>();

			category.Id = Guid.NewGuid().ToString();

			categories.Add(category);

			FileUtils.SaveListToFile(categories);

			return category;
		}

		public Category Update(Category category)
		{
			var categories = FileUtils.GetListFromFile<Category>();

			if (!categories.Any(c => c.Id == category.Id))
				throw new ArgumentException($"No category was found with {nameof(category.Id)} = {category.Id}.");

			categories = categories.Select(c =>
			{
				if (c.Id == category.Id)
					c = category;

				return c;
			}).ToList();

			FileUtils.SaveListToFile(categories);

			return category;
		}

		public void Delete(string id)
		{
			var categories = FileUtils.GetListFromFile<Category>();

			if (!categories.Any(c => c.Id == id))
				throw new ArgumentException($"No category was found with {nameof(id)} = {id}.");

			categories = categories.Where(c => c.Id != id).ToList();

			FileUtils.SaveListToFile(categories);
		}
	}
}
