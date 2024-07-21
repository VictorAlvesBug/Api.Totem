using Api.Totem.Application.DTOs.OrderItems;
using Api.Totem.Application.DTOs.Orders;
using Api.Totem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Totem.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderService;

		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		[HttpGet]
		[Route("")]
		public ActionResult List()
		{
			try
			{
				return Ok(_orderService.List());
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
				return Ok(_orderService.Get(id));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		[Route("")]
		public ActionResult Create()
		{
			try
			{
				return StatusCode(
					StatusCodes.Status201Created,
					_orderService.Create()
				);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		[Route("")]
		public ActionResult SetType(string id, OrderToSetTypeDTO orderToSetTypeDTO)
		{
			try
			{
				return StatusCode(
					StatusCodes.Status200OK,
					_orderService.SetType(id, orderToSetTypeDTO)
				);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		[Route("{id}/Items")]
		public ActionResult AddItem(string id, OrderItemToSaveDTO orderItemToSaveDTO)
		{
			try
			{
				return StatusCode(
					StatusCodes.Status200OK,
					_orderService.AddItem(id, orderItemToSaveDTO)
				);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPatch]
		[Route("{id}/Items/{orderItemId}")]
		public ActionResult UpdateItem(string id, string orderItemId, OrderItemToSaveDTO orderItemToSaveDTO)
		{
			try
			{
				return StatusCode(
					StatusCodes.Status200OK,
					_orderService.UpdateItem(id, orderItemId, orderItemToSaveDTO)
				);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete]
		[Route("{id}/Items/{orderItemId}")]
		public ActionResult RemoveItem(string id, string orderItemId)
		{
			try
			{
				return StatusCode(
					StatusCodes.Status200OK,
					_orderService.RemoveItem(id, orderItemId)
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
				_orderService.Delete(id);
				return StatusCode(StatusCodes.Status204NoContent);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
