using Api.Totem.Domain.Entities.Products;
using Api.Totem.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Totem.Application.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProductController : ControllerBase
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

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet(Name = "ListDrinks")]
		public IEnumerable<Drink> ListDrinks()
		{
			return _productService.List<Drink>();
		}

		[HttpPost(Name = "CreateDrink")]
		public Drink CreateDrink(Drink drink)
		{
			return _productService.Create(drink);
		}
	}
}