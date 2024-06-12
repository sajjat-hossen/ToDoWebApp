using ToDo.DomainLayer.Models;

namespace ToDo.RepositoryLayer.IRepository
{
    public interface ILabelRepository : IRepository<Label>
    {
        void Update(Label label);
        Task SaveAsync();
    }
}
