using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Northwind.Store.Model/*.Metadata*/
{
	[ModelMetadataType(typeof(CategoryMetadata))]
	public partial class Category
	{
		public class CategoryMetadata
		{
			[Required(ErrorMessage = "El nombre es requerido.")]
			[Display(Name = "Nombre")]
			public string CategoryName { get; set; }

			[Required(ErrorMessage = "La descripción es requerida.")]
			[Display(Name = "Descripción")]
			public string Description { get; set; }
		}
	}

}
