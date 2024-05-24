using ToDo.DataAccess.Data;
using ToDo.DataAccess.Repository.IRepository;
using ToDo.Models;

namespace ToDo.DataAccess.Repository
{
    public class ToDoTaskRepository : Repository<ToDoTask>, IToDoTaskRepository
    {
        #region Properties

        private ApplicationDbContext dbContext;

        #endregion

        #region CTOR
        public ToDoTaskRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        #endregion

        #region Save

        public void Save()
        {
            dbContext.SaveChanges();
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
