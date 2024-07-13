using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Interfaces.Repositories;
using Api.Totem.Helpers.Extensions;
using Api.Totem.Infrastructure.Utils;

namespace Api.Totem.Infrastructure.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		public IEnumerable<Category> List()
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

			FileUtils.SaveListToFile(categories.Append(category));

			return category;
		}

		public Category Update(Category category)
		{
			var categories = FileUtils.GetListFromFile<Category>();

			if (!categories.SafeAny(c => c.Id == category.Id))
				throw new ArgumentException($"No category was found with {nameof(category.Id)} = {category.Id}.");

			categories = categories.Where(item => item.Id != category.Id);

			FileUtils.SaveListToFile(categories.Append(category));

			return category;
		}

		public void Delete(string id)
		{
			var categories = FileUtils.GetListFromFile<Category>();

			if (!categories.SafeAny(c => c.Id == id))
				throw new ArgumentException($"No category was found with {nameof(id)} = {id}.");

			categories = categories.Where(category => category.Id != id);

			FileUtils.SaveListToFile(categories);
		}
	}
}
