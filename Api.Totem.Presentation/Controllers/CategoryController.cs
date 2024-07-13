using Api.Totem.Application.DTOs.Categories;
using Api.Totem.Application.Interfaces;
using Api.Totem.Helpers.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Totem.Presentation.Controllers
{
	[ApiController]
	[Route("Categories")]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet]
		[Route("")]
		public ActionResult List()
		{
			try
			{
				return Ok(_categoryService.List());
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
				return Ok(_categoryService.Get(id));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		[Route("")]
		public ActionResult Create(CategoryToCreateDTO categoryToCreateDTO)
		{
			try
			{
				categoryToCreateDTO.Validate();

				  return StatusCode(
					StatusCodes.Status201Created,
					_categoryService.Create(categoryToCreateDTO)
				);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPatch]
		[Route("{id}")]
		public ActionResult Update(string id, CategoryToUpdateDTO categoryToUpdateDTO)
		{
			try
			{
				return StatusCode(
					StatusCodes.Status200OK,
					_categoryService.Update(id, categoryToUpdateDTO)
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
				_categoryService.Delete(id);
				return StatusCode(StatusCodes.Status204NoContent);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		private string GetModelStateErrors(ModelStateDictionary modelState)
		{
			var errors = modelState
				.Where(ms => ms.Value?.Errors.Any() ?? false)
				.SelectMany(ms => ms.Value.Errors)
				.Select(e => e.ErrorMessage);

			return errors?.JoinThis() ?? string.Empty;
		}
	}
}