using ToDo.DomainLayer.Models;

namespace ToDo.RepositoryLayer.IRepository
{
    public interface IToDoTaskRepository : IRepository<ToDoTask>
    {
        void Update(ToDoTask toDoTask);
        Task SaveAsync();
    }
}
