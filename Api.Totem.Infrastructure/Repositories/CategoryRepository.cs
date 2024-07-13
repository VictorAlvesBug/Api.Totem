using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Interfaces.Repositories;
using Api.Totem.Helpers.Extensions;
using Api.Totem.Infrastructure.Utils;

namespace Api.Totem.Infrastructure.Repositories
{
	public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
	{
	}
}
