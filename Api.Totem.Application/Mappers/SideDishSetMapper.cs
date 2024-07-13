using Api.Totem.Application.DTOs.SideDishSets;
using Api.Totem.Domain.Entities;
using Api.Totem.Helpers.Extensions;

namespace Api.Totem.Application.Mappers
{
	public static class SideDishSetMapper
	{
		public static SideDishSetDTO MapToSideDishSetDTO(this SideDishSetToCreateDTO sideDishSetToCreateDTO)
		{
			return sideDishSetToCreateDTO.ConvertTo<SideDishSetDTO>();
		}

		public static IEnumerable<SideDishSetDTO> MapToSideDishSetDTO(this IEnumerable<SideDishSetToCreateDTO> sideDishSetsToCreateDTO)
		{
			return sideDishSetsToCreateDTO.Select(sideDishSetToCreateDTO => sideDishSetToCreateDTO.MapToSideDishSetDTO());
		}

		public static SideDishSet MapToSideDishSet(this SideDishSetDTO sideDishSetDTO)
		{
			return sideDishSetDTO.ConvertTo<SideDishSet>();
		}

		public static IEnumerable<SideDishSet> MapToSideDishSet(this IEnumerable<SideDishSetDTO> sideDishSetsDTO)
		{
			return sideDishSetsDTO.Select(sideDishSetDTO => sideDishSetDTO.MapToSideDishSet());
		}

		public static SideDishSet MapToSideDishSet(this SideDishSetToCreateDTO sideDishSetToCreateDTO)
		{
			return sideDishSetToCreateDTO.ConvertTo<SideDishSet>();
		}

		public static IEnumerable<SideDishSet> MapToSideDishSet(this IEnumerable<SideDishSetToCreateDTO> sideDishSetsToCreateDTO)
		{
			return sideDishSetsToCreateDTO.Select(sideDishSetToCreateDTO => sideDishSetToCreateDTO.MapToSideDishSet());
		}
	}
}
