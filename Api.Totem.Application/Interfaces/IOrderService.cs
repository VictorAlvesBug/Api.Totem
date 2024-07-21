
using Api.Totem.Application.DTOs.OrderItems;
using Api.Totem.Application.DTOs.Orders;

namespace Api.Totem.Application.Interfaces
{
	public interface IOrderService
	{
		IEnumerable<OrderToShowDTO> List();
		OrderToShowDTO Get(string id);
		OrderToShowDTO Create();
		OrderToShowDTO SetType(string id, OrderToSetTypeDTO orderToSetTypeDTO);
		OrderToShowDTO AddItem(string id, OrderItemToSaveDTO orderItemToSaveDTO);
		OrderToShowDTO UpdateItem(string id, string orderItemId, OrderItemToSaveDTO orderItemToSaveDTO);
		OrderToShowDTO RemoveItem(string id, string orderItemId);
		void Delete(string id);
	}
}
