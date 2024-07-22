using Api.Totem.Application.DTOs.Products;
using Api.Totem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Totem.Presentation.Controllers
{
	[ApiController]
	[Route("products")]
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
				return Ok(_productService.List());
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
				return Ok(_productService.Get(id));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		[Route("")]
		public ActionResult Create(ProductToCreateDTO productToCreateDTO)
		{
			try
			{
				return StatusCode(
					StatusCodes.Status201Created,
					_productService.Create(productToCreateDTO)
				);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		[Route("{id}")]
		public ActionResult Update(string id, ProductToUpdateDTO productToUpdateDTO)
		{
			try
			{
				return StatusCode(
					StatusCodes.Status200OK,
					_productService.Update(id, productToUpdateDTO)
				);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPatch]
		[Route("{id}/availability")]
		public ActionResult UpdateAvailability(string id, ProductToUpdateAvailabilityDTO productToUpdateAvailabilityDTO)
		{
			try
			{
				return StatusCode(
					StatusCodes.Status200OK,
					_productService.UpdateAvailability(id, productToUpdateAvailabilityDTO)
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