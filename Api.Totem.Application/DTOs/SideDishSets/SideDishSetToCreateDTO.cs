using System.ComponentModel.DataAnnotations;

namespace Api.Totem.Application.DTOs.SideDishSets
{
	public class SideDishSetToCreateDTO
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = $"The {nameof(Amount)} must have a positive non-zero value.")]
		public int Amount { get; set; }

		[Required]
		public string CategoryId { get; set; }
	}
}
