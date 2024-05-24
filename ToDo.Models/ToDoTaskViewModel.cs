using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Models
{
    public class ToDoTaskViewModel
    {
        public IEnumerable<ToDoTask> ToDoTasks { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalTasks { get; set; }
        public string QueryTerm { get; set; }
    }
}
