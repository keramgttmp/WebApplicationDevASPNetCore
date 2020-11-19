
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using P_Quiz.Models;

namespace P_Quiz.ViewModels

{
    public class ToDoViewModel
    {
        public string Filter { get; set; }
        public List<ToDo> ToDo{ get; set; }

        public int TotalCount { get; set; }
        public int PageSize => 4;
        public int PageCount => TotalCount / PageSize;

        /// <summary>
        /// Página actual
        /// </summary>
        public int Page { get; set; } = 1;
    }
}
