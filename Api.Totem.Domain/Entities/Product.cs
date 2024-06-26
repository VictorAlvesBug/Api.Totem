﻿using Api.Totem.Domain.Enumerators;

namespace Api.Totem.Domain.Entities
{
	public class Product
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public bool Available { get; set; }
    }
}
