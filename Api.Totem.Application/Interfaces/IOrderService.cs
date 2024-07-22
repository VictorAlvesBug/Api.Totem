
using Api.Totem.Application.DTOs.OrderItems;
using Api.Totem.Application.DTOs.Orders;
using Api.Totem.Domain.Enumerators;

namespace Api.Totem.Application.Interfaces
{
	public interface IOrderService
	{
		IEnumerable<OrderToShowDTO> List();
		OrderToShowDTO Get(string id);
		OrderToShowDTO Create();
		OrderToShowDTO SetType(string id, OrderType orderType);
		OrderToShowDTO SetPaymentMethod(string id, PaymentMethod paymentMethod);
		OrderToShowDTO SetPagerId(string id, int pagerId);
		OrderToShowDTO SetComment(string id, string comment);
		OrderToShowDTO Confirm(string id);
		OrderToShowDTO AddItem(string id, OrderItemToSaveDTO orderItemToSaveDTO);
		OrderToShowDTO UpdateItem(string id, string orderItemId, OrderItemToSaveDTO orderItemToSaveDTO);
		OrderToShowDTO RemoveItem(string id, string orderItemId);
		void Delete(string id);
	}
}
