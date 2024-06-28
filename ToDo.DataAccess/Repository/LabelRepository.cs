using ToDo.DataAccess.Data;
using ToDo.DomainLayer.Models;
using ToDo.RepositoryLayer.IRepository;

namespace ToDo.DataAccess.Repository
{
    public class LabelRepository : Repository<Label>,  ILabelRepository
    {
        #region Properties

        private ApplicationDbContext _dbContext;

        #endregion

        #region Constructor

        public LabelRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region SaveAsync

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        #endregion

        #region Update

        public void Update(Label label)
        {
            _dbContext.Labels.Update(label);
        }

        #endregion
    }
}
