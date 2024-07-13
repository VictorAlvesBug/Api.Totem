﻿using Api.Totem.Domain.Entities;

namespace Api.Totem.Application.DTOs.Products
{
	public class ProductToShowDTO
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public bool Available { get; set; }
    }
}
