using ToDo.DomainLayer.Models;

namespace ToDo.ServiceLayer.IServices
{
    public interface ILabelService
    {
        IEnumerable<Label> GetAllLabelFromDb();
        Task CreateNewLabelAsync(Label label);
        Task<Label> GetFirstLabelFromDbBySearchAsync(int? id);
        Task UpdateLabelAsync(Label label);
        Task DeleteLabelAsync(Label label);
    }
}
