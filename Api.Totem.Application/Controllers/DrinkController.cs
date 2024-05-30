using Api.Totem.Application.DTOs;
using Api.Totem.Application.DTOs.Products;
using Api.Totem.Domain.Entities.Products;
using Api.Totem.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Totem.Application.Controllers
{
	[ApiController]
	[Route("Drinks")]
	public class DrinkController : ControllerBase
	{
		/*
		
		- Product
		  Id, Name, Description, CategoryId, Price, Available
		
		- FixedMeal : Product

		- MainDish : Product
		  SideDishesAmountAllowed: SideDishSet[]
		  SoldSeparatelyPrice

		- SideDish : Product
		  SideDishType
		  SoldSeparatelyPrice

		- Drink : Product

		- Category
		  Id, Name

		- SideDishSet
		  SideDishType, Amount

		- OrderType
		  FixedMeal, MainDishWithSides
		
		- PaymentType
		  CreditCard, DebitCard, Voucher, Pix, Cash

		- SideDishType
		  Normal, Special

		- Order
		  

		- Statement
		  

		*/

		private readonly IProductService _productService;

		public DrinkController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		[Route("")]
		public ActionResult List()
		{
			try
			{
				return Ok(_productService.List<Drink>().Select(drink => new DrinkToShowDTO(drink)));
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
					new DrinkToShowDTO(_productService.Get<Drink>(id))
				);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		[Route("")]
		public ActionResult Create(DrinkToCreateDTO drinkToCreate)
		{
			try
			{
				return StatusCode(
					StatusCodes.Status201Created,
					new DrinkToShowDTO(_productService.Create(drinkToCreate.ToDrink()))
				);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPatch]
		[Route("{id}")]
		public ActionResult Update(string id, DrinkToUpdateDTO drinkToUpdate)
		{
			try
			{
				return StatusCode(
					StatusCodes.Status200OK,
					new DrinkToShowDTO(_productService.Update(drinkToUpdate.ToDrink(id)))
				);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPatch]
		[Route("{id}/Availability")]
		public ActionResult UpdateAvailability(string id, DrinkToUpdateAvailabilityDTO drinkToUpdateAvailability)
		{
			try
			{
				return StatusCode(
					StatusCodes.Status200OK,
					new DrinkToShowDTO(_productService.UpdateAvailability(drinkToUpdateAvailability.ToDrink(id)))
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
				_productService.Delete<Drink>(id);
				return StatusCode(StatusCodes.Status204NoContent);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}