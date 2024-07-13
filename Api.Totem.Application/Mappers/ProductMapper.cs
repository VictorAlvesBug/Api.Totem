using Api.Totem.Application.DTOs.Products;
using Api.Totem.Domain.Entities;
using Api.Totem.Helpers.Extensions;

namespace Api.Totem.Application.Mappers
{
	public static class ProductMapper
	{
		public static ProductDTO MapToProductDTO(this Product product)
		{
			return product.ConvertTo<ProductDTO>();
		}

		public static IEnumerable<ProductDTO> MapToProductDTO(this IEnumerable<Product> products)
		{
			return products.Select(product => MapToProductDTO(product));
		}

		public static ProductToShowDTO MapToProductToShowDTO(this ProductDTO productDTO)
		{
			return productDTO.ConvertTo<ProductToShowDTO>();
		}

		public static IEnumerable<ProductToShowDTO> MapToProductToShowDTO(this IEnumerable<ProductDTO> productsDTO)
		{
			return productsDTO.Select(productDTO => MapToProductToShowDTO(productDTO));
		}

		public static ProductToShowDTO MapToProductToShowDTO(this Product product)
		{
			return product.ConvertTo<ProductToShowDTO>();
		}

		public static IEnumerable<ProductToShowDTO> MapToProductToShowDTO(this IEnumerable<Product> products)
		{
			return products.Select(product => MapToProductToShowDTO(product));
		}

		public static Product MapToProduct(this ProductDTO productDTO)
		{
			return productDTO.ConvertTo<Product>();
		}

		public static IEnumerable<Product> MapToProduct(this IEnumerable<ProductDTO> productsDTO)
		{
			return productsDTO.Select(productDTO => MapToProduct(productDTO));
		}

		public static Product MapToProduct(this ProductToCreateDTO productToCreateDTO)
		{
			return productToCreateDTO.ConvertTo<Product>();
		}

		public static IEnumerable<Product> MapToProduct(this IEnumerable<ProductToCreateDTO> productsToCreateDTO)
		{
			return productsToCreateDTO.Select(productToCreateDTO => MapToProduct(productToCreateDTO));
		}

		public static Product MapToProduct(this ProductToUpdateDTO productToUpdateDTO)
		{
			return productToUpdateDTO.ConvertTo<Product>();
		}

		public static IEnumerable<Product> MapToProduct(this IEnumerable<ProductToUpdateDTO> productsToUpdateDTO)
		{
			return productsToUpdateDTO.Select(productToUpdateDTO => MapToProduct(productToUpdateDTO));
		}

		public static Product MapToProduct(this ProductToUpdateAvailabilityDTO productToUpdateAvailabilityDTO)
		{
			return productToUpdateAvailabilityDTO.ConvertTo<Product>();
		}

		public static IEnumerable<Product> MapToProduct(this IEnumerable<ProductToUpdateAvailabilityDTO> productsToUpdateAvailabilityDTO)
		{
			return productsToUpdateAvailabilityDTO.Select(productToUpdateAvailabilityDTO => MapToProduct(productToUpdateAvailabilityDTO));
		}
	}
}
