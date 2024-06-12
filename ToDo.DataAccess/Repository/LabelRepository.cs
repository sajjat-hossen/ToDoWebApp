using ToDo.DataAccess.Data;
using ToDo.DomainLayer.Models;
using ToDo.RepositoryLayer.IRepository;

namespace ToDo.DataAccess.Repository
{
    public class LabelRepository : Repository<Label>,  ILabelRepository
    {
        #region Properties

        private ApplicationDbContext dbContext;

        #endregion

        #region Constructor

        public LabelRepository(ApplicationDbContext dbContext) : base(dbContext)
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

        public void Update(Label label)
        {
            dbContext.Labels.Update(label);
        }

        #endregion
    }
}
