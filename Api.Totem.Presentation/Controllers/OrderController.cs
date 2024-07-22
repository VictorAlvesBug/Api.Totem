using Api.Totem.Application.DTOs.OrderItems;
using Api.Totem.Application.DTOs.Orders;
using Api.Totem.Application.Interfaces;
using Api.Totem.Domain.Enumerators;
using Microsoft.AspNetCore.Mvc;

namespace Api.Totem.Presentation.Controllers
{
	[ApiController]
	[Route("orders")]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderService;

		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		#region Get and List
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
		#endregion

		#region Create
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
		#endregion

		#region Update
		[HttpPatch]
		[Route("{id}/type")]
		public ActionResult SetType(string id, OrderType orderType)
		{
			try
			{
				return StatusCode(
					StatusCodes.Status200OK,
					_orderService.SetType(id, orderType)
				);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPatch]
		[Route("{id}/payment")]
		public ActionResult SetPaymentMethod(string id, PaymentMethod paymentMethod)
		{
			try
			{
				return StatusCode(
					StatusCodes.Status200OK,
					_orderService.SetPaymentMethod(id, paymentMethod)
				);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPatch]
		[Route("{id}/pager")]
		public ActionResult SetPagerId(string id, int pagerId)
		{
			try
			{
				return StatusCode(
					StatusCodes.Status200OK,
					_orderService.SetPagerId(id, pagerId)
				);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPatch]
		[Route("{id}/comment")]
		public ActionResult SetComment(string id, string comment)
		{
			try
			{
				return StatusCode(
					StatusCodes.Status200OK,
					_orderService.SetComment(id, comment)
				);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPatch]
		[Route("{id}")]
		public ActionResult Confirm(string id)
		{
			try
			{
				return StatusCode(
					StatusCodes.Status200OK,
					_orderService.Confirm(id)
				);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		#endregion

		#region OrderItem
		[HttpPost]
		[Route("{id}/items")]
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

		[HttpPut]
		[Route("{id}/items/{orderItemId}")]
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
		[Route("{id}/items/{orderItemId}")]
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
		#endregion

		#region Delete
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
		#endregion
	}
}
