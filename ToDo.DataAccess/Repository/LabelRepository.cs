using ToDo.DataAccess.Data;
using ToDo.DataAccess.Repository.IRepository;
using ToDo.Models;

namespace ToDo.DataAccess.Repository
{
    public class LabelRepository : Repository<Label>,  ILabelRepository
    {
        #region Properties
        private ApplicationDbContext dbContext;

        #endregion

        #region CTOR
        public LabelRepository(ApplicationDbContext dbContext) : base(dbContext)
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

        public void Update(Label label)
        {
            dbContext.Labels.Update(label);
        }

        #endregion
    }
}
