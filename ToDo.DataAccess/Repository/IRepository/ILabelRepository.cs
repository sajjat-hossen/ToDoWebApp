using ToDo.Models;

namespace ToDo.DataAccess.Repository.IRepository
{
    public interface ILabelRepository : IRepository<Label>
    {
        void Update(Label label);
        Task SaveAsync();
    }
}
