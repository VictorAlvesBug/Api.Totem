using Api.Totem.Application.DTOs;
using Api.Totem.Application.DTOs.Products;
using Api.Totem.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Totem.Application.Controllers
{
	[ApiController]
	[Route("Products")]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		[Route("")]
		public ActionResult List()
		{
			try
			{
				return Ok(_productService.List().Select(product => new ProductToShowDTO(product)));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		[Route("{id}")]
		public ActionResult Get(string id)
		{
			try
			{
				return Ok(
					new ProductToShowDTO(_productService.Get(id))
				);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		[Route("")]
		public ActionResult Create(ProductToCreateDTO productToCreate)
		{
			try
			{
				return StatusCode(
					StatusCodes.Status201Created,
					new ProductToShowDTO(_productService.Create(productToCreate.ToProduct()))
				);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPatch]
		[Route("{id}")]
		public ActionResult Update(string id, ProductToUpdateDTO productToUpdate)
		{
			try
			{
				return StatusCode(
					StatusCodes.Status200OK,
					new ProductToShowDTO(_productService.Update(productToUpdate.ToProduct(id)))
				);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPatch]
		[Route("{id}/Availability")]
		public ActionResult UpdateAvailability(string id, ProductToUpdateAvailabilityDTO productToUpdateAvailability)
		{
			try
			{
				return StatusCode(
					StatusCodes.Status200OK,
					new ProductToShowDTO(_productService.UpdateAvailability(productToUpdateAvailability.ToProduct(id)))
				);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete]
		[Route("{id}")]
		public ActionResult Delete(string id)
		{
			try
			{
				_productService.Delete(id);
				return StatusCode(StatusCodes.Status204NoContent);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}