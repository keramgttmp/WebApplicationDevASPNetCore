﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Northwind.Store.Model;

namespace WA4.ViewModels
{
	public class CategoryProductsViewModel
	{
		public string CategoryName { get; set; }
		public List<Product> Items { get; set; }

		public CategoryProductsViewModel()
		{
			Items = new List<Product>();
		}
	}
}