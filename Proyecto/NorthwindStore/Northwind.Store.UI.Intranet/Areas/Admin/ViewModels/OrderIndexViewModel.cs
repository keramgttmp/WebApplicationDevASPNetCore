﻿using Northwind.Store.Data;
using Northwind.Store.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Store.UI.Intranet.Areas.Admin.ViewModels
{
    public class OrderIndexViewModel
    {
        OrderRepository _oR;

        public string Command { get; set; } = "list";
        public Order Filter { get; set; } = new Order();
        public IEnumerable<Order> Items { get; set; } = new List<Order>();
        public PageFilter Paging { get; set; } = new PageFilter() { Sort = "orderdate" , SortDir ="DESC", PageSize =10};

        public async Task HandleRequest(OrderRepository oR)
        {   
            _oR = oR;
            
            switch (Command)
            {
                case "list":
                    await list();
                    break;
                case "search":
                case "paging":
                case "order":
                    await search();
                    break;
                default:
                    break;
            }
        }

        async Task list()
        {
            Items = await _oR.GetList(Paging);
        }

        async Task search() 
        {
            Items = await _oR.Search(Filter.Customer.ContactName, Paging);
        }
    }
}