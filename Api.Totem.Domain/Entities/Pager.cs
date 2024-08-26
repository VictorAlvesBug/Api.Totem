using Api.Totem.Domain.Enumerators;

namespace Api.Totem.Domain.Entities
{
	public class Pager
	{
        public int Id { get; set; }
        public PagerStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
