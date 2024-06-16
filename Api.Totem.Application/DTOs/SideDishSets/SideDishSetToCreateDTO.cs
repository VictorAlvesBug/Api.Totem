using Api.Totem.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Api.Totem.Application.DTOs.SideDishSets
{
	public class SideDishSetToCreateDTO
	{
		[Required, Range(1, 5, ErrorMessage = $"The {nameof(Amount)} value must be between 1 and 5.")]
		public int Amount { get; set; }

		[Required]
		public string CategoryId { get; set; }

		public SideDishSet ToSideDishSet() => new SideDishSet
		{
			Amount = Amount,
			CategoryId = CategoryId
		};
	}
}
