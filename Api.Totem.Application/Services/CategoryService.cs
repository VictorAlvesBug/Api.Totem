using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Interfaces.Repositories;
using Api.Totem.Domain.Interfaces.Services;
using Api.Totem.Infrastructure.Utils;

namespace Api.Totem.Application.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;
		private readonly IProductRepository _productRepository;

		public CategoryService(
			ICategoryRepository categoryRepository,
			IProductRepository productRepository)
		{
			_categoryRepository = categoryRepository;
			_productRepository = productRepository;
		}

		public List<Category> List()
		{
			var categories = _categoryRepository.List();

			FillAdditionalProperties(categories);

			return categories;
		}

		public Category Get(string id)
		{
			var category = _categoryRepository.Get(id);

			FillAdditionalProperties(category);

			return category;
		}

		public Category Create(Category category)
		{
			category = _categoryRepository.Create(category);

			FillAdditionalProperties(category);

			return category;
		}

		public Category Update(Category category)
		{
			category = _categoryRepository.Update(category);

			FillAdditionalProperties(category);

			return category;
		}

		public void Delete(string id)
		{
			_categoryRepository.Delete(id);
		}

		public void FillAdditionalProperties(
			Category category,
			List<Product>? allProducts = null,
			List<Category>? allCategories = null)
		{
			allProducts ??= _productRepository.List();
			allCategories ??= _categoryRepository.List();

			if (category.ProductIds is not null)
			{
				category.Products = allProducts
					.Where(product => category.ProductIds.Contains(product.Id))
					.ToList();
			}

			if (category.SideDishSets is not null)
			{
				category.SideDishSets.ForEach(sideDishSet =>
				{
					sideDishSet.Category = allCategories.FirstOrDefault(cat => cat.Id == sideDishSet.CategoryId)
					 ?? throw new Exception($"No {nameof(category)} was found with {nameof(sideDishSet.CategoryId)} = '{sideDishSet.CategoryId}'");
				});
			}

			if (category.ComboItemCategoryIds is not null)
			{
				category.ComboItemCategories = allCategories
					.Where(cat => category.ComboItemCategoryIds.Contains(cat.Id))
					.ToList();
			}
		}

		public void FillAdditionalProperties(List<Category> categories)
		{
			var allProducts = _productRepository.List();
			var allCategories = _categoryRepository.List();

			categories.ForEach(category => FillAdditionalProperties(category, allProducts, allCategories));
		}
	}
}
