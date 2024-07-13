namespace Api.Totem.Infrastructure.Models
{
	public class Response<TData>
	{
        public IEnumerable<TData> Data { get; set; }

        public Response()
        {
            
        }

		public Response(IEnumerable<TData> data)
		{
			Data = data;
		}
	}
}
