using ToDo.DataAccess.Data;
using ToDo.DomainLayer.Models;
using ToDo.RepositoryLayer.IRepository;

namespace ToDo.DataAccess.Repository
{
    public class ToDoTaskRepository : Repository<ToDoTask>, IToDoTaskRepository
    {
        #region Properties

        private ApplicationDbContext dbContext;

        #endregion

        #region Constructor
        public ToDoTaskRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        #endregion

        #region SaveAsync

        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        #endregion

        #region Update

        public void Update(ToDoTask toDoTask)
        {
            dbContext.ToDoTasks.Update(toDoTask);
        }

        #endregion
    }
}
