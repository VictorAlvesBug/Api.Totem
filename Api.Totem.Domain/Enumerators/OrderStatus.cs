namespace Api.Totem.Domain.Enumerators
{
	public enum OrderStatus
	{
		Creating = 1,
		Ordered = 2,
		InQueue = 3,
		InProgress = 4,
		Ready = 5,
		Delivered = 6,
		Cancelled = 7
	}
}
