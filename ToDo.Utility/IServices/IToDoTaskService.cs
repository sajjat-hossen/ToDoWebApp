using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.DomainLayer.Models;

namespace ToDo.ServiceLayer.IServices
{
    public interface IToDoTaskService
    {
        IEnumerable<ToDoTask> GetAllToDoTaskFromDb();

        Task<ToDoTaskViewModel> GetDoTaskViewModelFromDbAsync(string queryTerm, int currentPage, int pageSize);
        ToDoTaskViewModel MakeToDoTaskViewModel(IEnumerable<ToDoTask> toDoTasks, string queryTerm, int currentPage, int pageSize);
        Task<IEnumerable<ToDoTask>> GetAllToDoTaskFromDbBySearchAsync(string queryTerm);
        IEnumerable<SelectListItem> GetLabelList();
        Task CreateNewToDoTaskAsync(ToDoTask toDoTask);
        Task<ToDoTask> GetFirstToDoTaskFromDbBySearchAsync(int? id);
        Task UpdateToDoTaskAsync(ToDoTask toDoTask);
        Task DeleteToDoTaskAsync(ToDoTask todoTask);
        IEnumerable<ToDoTask> GetAllCompletedToDoTaskFromDb();
        Task DeleteRangeToDoTaskAsync(IEnumerable<ToDoTask> toDoTaskFromDb);

    }
}
