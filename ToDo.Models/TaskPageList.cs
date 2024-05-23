using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Models
{
    public class TaskPageList
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public List<ToDoTask> Tasks { get;}
    }
}
