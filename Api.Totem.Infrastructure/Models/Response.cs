namespace Api.Totem.Infrastructure.Models
{
	public class Response<TData>
	{
        public List<TData> Data { get; set; }

        public Response()
        {
            
        }

		public Response(List<TData> data)
		{
			Data = data;
		}
	}
}
