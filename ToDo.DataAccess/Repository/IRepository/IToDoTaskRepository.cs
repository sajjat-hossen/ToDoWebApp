using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.DataAccess.Repository.IRepository
{
    public interface IToDoTaskRepository : IRepository<ToDoTask>
    {
        void Update(ToDoTask toDoTask);
        void Save();
    }
}
