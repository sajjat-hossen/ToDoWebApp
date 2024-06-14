using ToDo.DomainLayer.Models;
using ToDo.RepositoryLayer.IRepository;
using ToDo.ServiceLayer.IServices;

namespace ToDo.ServiceLayer.Services
{
    public class LabelService : ILabelService
    {
        #region Properties

        private readonly ILabelRepository labelRepository;

        #endregion

        #region Constructor

        public LabelService(ILabelRepository labelRepository)
        {
            this.labelRepository = labelRepository;
        }

        #endregion

        #region CreateNewLabelAsync

        public async Task CreateNewLabelAsync(Label label)
        {
            await labelRepository.AddAsync(label);
            await labelRepository.SaveAsync();
        }

        #endregion

        #region DeleteLabelAsync

        public async Task DeleteLabelAsync(Label label)
        {
            labelRepository.Remove(label);
            await labelRepository.SaveAsync();
        }

        #endregion

        #region GetAllLabelFromDb

        public IEnumerable<Label> GetAllLabelFromDb()
        {
            var labels = labelRepository.GetAllEntityFromDb(x => true).ToList();

            return labels;
        }

        #endregion

        #region GetFirstLabelFromDbBySearchAsync

        public async Task<Label> GetFirstLabelFromDbBySearchAsync(int? id)
        {
            var labelFromDb = await labelRepository.GetFirstEntityFromDbBySearchAsync(u => u.Id == id);

            return labelFromDb;
        }

        #endregion

        #region UpdateLabelAsync

        public async Task UpdateLabelAsync(Label label)
        {
            labelRepository.Update(label);
            await labelRepository.SaveAsync();
        }

        #endregion
    }
}
