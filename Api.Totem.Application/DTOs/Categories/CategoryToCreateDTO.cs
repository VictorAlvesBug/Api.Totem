﻿using Api.Totem.Application.DTOs.SideDishSets;
using Api.Totem.Application.Validations;
using Api.Totem.Domain.Entities;
using Api.Totem.Domain.Enumerators;
using Api.Totem.Helpers.DataAnnotations.Attributes;
using Api.Totem.Helpers.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Api.Totem.Application.DTOs.Categories
{
	public class CategoryToCreateDTO
	{
		[Required, EnumValidation(typeof(CategoryType))]
		public CategoryType CategoryType { get; set; }

		[Required, StringLength(30)]
		public string Name { get; set; }

		[Required, EnumValidation(typeof(ComplementType))]
		public ComplementType ComplementType { get; set; }

		[WhenRequiredErrorMessage($"Categories with this {nameof(ComplementType)} must have a non-empty {nameof(SideDishSets)} list.")]
		public List<SideDishSetToCreateDTO>? SideDishSets { get; set; }

		[WhenRequiredErrorMessage($"Categories with this {nameof(ComplementType)} must have a non-empty {nameof(ComboItemCategoryIds)} list.")]
		public List<string>? ComboItemCategoryIds { get; set; }

		[Range(0, double.MaxValue, ErrorMessage = $"The {nameof(ComboAdditionalPrice)} must have a positive value."), 
		WhenRequiredErrorMessage($"Categories with this {nameof(ComplementType)} must have a {nameof(ComboAdditionalPrice)} value.")]
		public decimal? ComboAdditionalPrice { get; set; }

		public void Validate()
		{
			switch (ComplementType)
			{
				case ComplementType.None:
					return;

				case ComplementType.SideDishes:
					if (!SideDishSets.SafeAny())
						throw new Exception(this.GetErrorMessageFor(nameof(SideDishSets)));
					return;

				case ComplementType.OptionalCombo:
					if (!ComboItemCategoryIds.SafeAny())
						throw new Exception(this.GetErrorMessageFor(nameof(ComboItemCategoryIds)));

					if (ComboAdditionalPrice is null)
						throw new Exception(this.GetErrorMessageFor(nameof(ComboAdditionalPrice)));

					break;
			}
		}

		public Category ToCategory()
		{
			var category = new Category
			{
				CategoryType = CategoryType,
				Name = Name,
				ProductIds = new List<string>(),
				ComplementType = ComplementType
			};

			switch (category.ComplementType)
			{
				case ComplementType.SideDishes:
					category.SideDishSets = SideDishSets?.Select(sideDishSet => sideDishSet.ToSideDishSet()).ToList()
						?? throw new Exception(this.GetErrorMessageFor(nameof(SideDishSets)));
					break;

				case ComplementType.OptionalCombo:
					category.ComboItemCategoryIds = ComboItemCategoryIds
						?? throw new ArgumentNullException(this.GetErrorMessageFor(nameof(ComboItemCategoryIds)));
					category.ComboAdditionalPrice = ComboAdditionalPrice
						?? throw new ArgumentNullException(this.GetErrorMessageFor(nameof(ComboAdditionalPrice)));
					break;
			}

			return category;
		}
	}
}
