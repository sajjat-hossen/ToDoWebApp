using ToDo.Models;

namespace ToDo.DataAccess.Repository.IRepository
{
    public interface IToDoTaskRepository : IRepository<ToDoTask>
    {
        void Update(ToDoTask toDoTask);
        Task SaveAsync();
    }
}
