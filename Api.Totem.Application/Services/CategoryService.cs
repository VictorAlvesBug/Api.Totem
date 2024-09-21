using Api.Totem.Application.DTOs.Categories;
using Api.Totem.Application.DTOs.Products;
using Api.Totem.Application.DTOs.SideDishSets;
using Api.Totem.Application.Interfaces;
using Api.Totem.Application.Mappers;
using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Interfaces.Repositories;
using Api.Totem.Helpers.Extensions;

namespace Api.Totem.Application.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly IBaseRepository<Category> _categoryRepository;
		private readonly IBaseRepository<Product> _productRepository;
		private readonly IBaseRepository<SideDishSet> _sideDishSetRepository;
		private readonly IBaseRepository<CategorySideDishSet> _categorySideDishSetRepository;
		private readonly IBaseRepository<CategoryProduct> _categoryProductRepository;

		public CategoryService(
			IBaseRepository<Category> categoryRepository,
			IBaseRepository<Product> productRepository,
			IBaseRepository<SideDishSet> sideDishSetRepository,
			IBaseRepository<CategorySideDishSet> categorySideDishSetRepository,
			IBaseRepository<CategoryProduct> categoryProductRepository)
		{
			_categoryRepository = categoryRepository;
			_productRepository = productRepository;
			_sideDishSetRepository = sideDishSetRepository;
			_categorySideDishSetRepository = categorySideDishSetRepository;
			_categoryProductRepository = categoryProductRepository;
		}

		public IEnumerable<CategoryToShowDTO> List()
		{
			var categoriesDTO = _categoryRepository.List().MapToCategoryDTO();
			FillAdditionalPropertiesToShow(categoriesDTO);
			return categoriesDTO.MapToCategoryToShowDTO();
		}

		public CategoryToShowDTO Get(string id)
		{
			var categoryDTO = _categoryRepository.Get(id).MapToCategoryDTO();
			FillAdditionalPropertiesToShow(categoryDTO);
			return categoryDTO.MapToCategoryToShowDTO();
		}

		public CategoryToShowDTO Create(CategoryToCreateDTO categoryToCreateDTO)
		{
			if (categoryToCreateDTO is null)
				throw new ArgumentNullException(nameof(categoryToCreateDTO));

			var category = categoryToCreateDTO.MapToCategory();

			category.Id = Guid.NewGuid().ToString();

			var categoryDTO = _categoryRepository.Create(category).MapToCategoryDTO();
			FillAdditionalPropertiesToShow(categoryDTO);
			return categoryDTO.MapToCategoryToShowDTO();
		}

		public CategoryToShowDTO Update(string id, CategoryToUpdateDTO categoryToUpdateDTO)
		{
			if (categoryToUpdateDTO is null)
				throw new ArgumentNullException(nameof(categoryToUpdateDTO));

			var category = _categoryRepository.Get(id);

			category.CategoryType = categoryToUpdateDTO.CategoryType;
			category.Name = categoryToUpdateDTO.Name;
			category.ComplementType = categoryToUpdateDTO.ComplementType;
			category.ComboItemCategoryIds = categoryToUpdateDTO.ComboItemCategoryIds;
			category.ComboAdditionalPrice = categoryToUpdateDTO.ComboAdditionalPrice;

			SaveSideDishSets(id, categoryToUpdateDTO.SideDishSets);

			var categoryDTO = _categoryRepository.Update(category).MapToCategoryDTO();
			FillAdditionalPropertiesToShow(categoryDTO);
			return categoryDTO.MapToCategoryToShowDTO();
		}

		public void Delete(string id)
		{
			_categoryRepository.Delete(id);
		}

		public CategoryToShowDTO AddProducts(string id, CategoryProductsToAddDTO categoryProductsToAddDTO)
		{
			if (categoryProductsToAddDTO is null)
				throw new ArgumentNullException(nameof(categoryProductsToAddDTO));

			var category = _categoryRepository.Get(id);

			var productIdsNotFound = new List<string>();

			foreach (var productId in categoryProductsToAddDTO.ProductIds)
			{
				if (!_productRepository.TryGet(productId, out _))
					productIdsNotFound.Add(productId);
			}

			if (productIdsNotFound.SafeAny())
				throw new ArgumentException($"No products were found with the following {nameof(BaseEntity.Id)}(s): {productIdsNotFound.JoinThis()}.");

			category.ProductIds = category.ProductIds.Concat(categoryProductsToAddDTO.ProductIds);

			var categoryDTO = _categoryRepository.Update(category).MapToCategoryDTO();
			FillAdditionalPropertiesToShow(categoryDTO);
			return categoryDTO.MapToCategoryToShowDTO();
		}

		public CategoryToShowDTO RemoveProducts(string id, CategoryProductsToRemoveDTO categoryProductsToRemoveDTO)
		{
			if (categoryProductsToRemoveDTO is null)
				throw new ArgumentNullException(nameof(categoryProductsToRemoveDTO));

			var category = _categoryRepository.Get(id);

			var productIdsNotFound = new List<string>();
			
			foreach (var productId in categoryProductsToRemoveDTO.ProductIds)
			{
				if (!_productRepository.TryGet(productId, out _))
					productIdsNotFound.Add(productId);
			}

			if(productIdsNotFound.SafeAny())
				throw new ArgumentException($"No products were found with the following {nameof(BaseEntity.Id)}(s): {productIdsNotFound.JoinThis()}.");

			category.ProductIds = category.ProductIds
				.Where(productId => !categoryProductsToRemoveDTO.ProductIds.Contains(productId));

			var categoryDTO = _categoryRepository.Update(category).MapToCategoryDTO();
			FillAdditionalPropertiesToShow(categoryDTO);
			return categoryDTO.MapToCategoryToShowDTO();
		}

		private void FillAdditionalPropertiesToShow(
			CategoryDTO categoryDTO,
			IEnumerable<ProductDTO>? allProductsDTO = null,
			IEnumerable<CategoryDTO>? allCategoriesDTO = null)
		{
			allProductsDTO ??= _productRepository.List().ConvertTo<ProductDTO>();
			allCategoriesDTO ??= _categoryRepository.List().ConvertTo<CategoryDTO>();

			if (categoryDTO.ProductIds is not null)
			{
				categoryDTO.Products = allProductsDTO
					.Where(productDTO => categoryDTO.ProductIds.Contains(productDTO.Id))
					.ToList();
			}

			if (categoryDTO.SideDishSets is not null)
			{
				var categoryIdsNotFound = new List<string>();

                foreach (var sideDishSetDTO in categoryDTO.SideDishSets)
                {
					sideDishSetDTO.Category = allCategoriesDTO.FirstOrDefault(item => item.Id == sideDishSetDTO.CategoryId);

					if (sideDishSetDTO.Category == null)
						categoryIdsNotFound.Add(sideDishSetDTO.CategoryId);
				}

				if(categoryIdsNotFound.SafeAny())
					throw new Exception($"No {nameof(categoryDTO)}(s) were found with the following {nameof(BaseEntity.Id)}(s): {categoryIdsNotFound.JoinThis()}");
			}

			if (categoryDTO.ComboItemCategoryIds is not null)
			{
				categoryDTO.ComboItemCategories = allCategoriesDTO
					.Where(item => categoryDTO.ComboItemCategoryIds.Contains(item.Id))
					.ToList();
			}
		}

		private void FillAdditionalPropertiesToShow(IEnumerable<CategoryDTO> categoriesDTO)
		{
			var allProductsDTO = _productRepository.List().ConvertTo<ProductDTO>();
			var allCategoriesDTO = _categoryRepository.List().ConvertTo<CategoryDTO>();

			for (var i = 0; i < categoriesDTO.Count(); i++)
            {
				FillAdditionalPropertiesToShow(categoriesDTO.ElementAt(i), allProductsDTO, allCategoriesDTO);
			}
		}

		private void SaveSideDishSets(string categoryId, IEnumerable<SideDishSetToCreateDTO>? sideDishSetsToCreateDTO = null)
		{
			var categoryIdCondition = new Dictionary<string, dynamic>
				{
					{ nameof(CategorySideDishSet.CategoryId), categoryId }
				};

			var alreadySavedSideDishSetIds = 
				_categorySideDishSetRepository.List(categoryIdCondition)
				.Select(categorySideDishSet => categorySideDishSet.SideDishSetId);

			_categorySideDishSetRepository.Delete(categoryIdCondition);

			alreadySavedSideDishSetIds.ForEach(sideDishSetId =>
			{
				_sideDishSetRepository.Delete(
					new Dictionary<string, dynamic>
					{
						{ nameof(SideDishSet.Id), sideDishSetId }
					});
			});

			sideDishSetsToCreateDTO.MapToSideDishSet()
				.ForEach((sideDishSet) => {
					sideDishSet.Id = Guid.NewGuid().ToString();
					_sideDishSetRepository.Create(sideDishSet);
				});
		}
	}
}
