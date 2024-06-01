using Api.Totem.Application.DTOs;
using Api.Totem.Application.DTOs.Categories;
using Api.Totem.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Totem.Application.Controllers
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
				return Ok(_categoryService.List().Select(category => new CategoryToShowDTO(category)));
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
					new CategoryToShowDTO(_categoryService.Get(id))
				);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		[Route("")]
		public ActionResult Create(CategoryToCreateDTO categoryToCreate)
		{
			try
			{
				return StatusCode(
					StatusCodes.Status201Created,
					new CategoryToShowDTO(_categoryService.Create(categoryToCreate.ToCategory()))
				);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPatch]
		[Route("{id}")]
		public ActionResult Update(string id, CategoryToUpdateDTO categoryToUpdate)
		{
			try
			{
				return StatusCode(
					StatusCodes.Status200OK,
					new CategoryToShowDTO(_categoryService.Update(categoryToUpdate.ToCategory(id)))
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
	}
}