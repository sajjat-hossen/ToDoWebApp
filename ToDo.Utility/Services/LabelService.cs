using ToDo.DomainLayer.Models;
using ToDo.RepositoryLayer.IRepository;
using ToDo.ServiceLayer.IServices;

namespace ToDo.ServiceLayer.Services
{
    public class LabelService : ILabelService
    {
        #region Properties

        private readonly ILabelRepository _labelRepository;

        #endregion

        #region Constructor

        public LabelService(ILabelRepository labelRepository)
        {
            _labelRepository = labelRepository;
        }

        #endregion

        #region CreateNewLabelAsync

        public async Task CreateNewLabelAsync(Label label)
        {
            await _labelRepository.AddAsync(label);
            await _labelRepository.SaveAsync();
        }

        #endregion

        #region DeleteLabelAsync

        public async Task DeleteLabelAsync(Label label)
        {
            _labelRepository.Remove(label);
            await _labelRepository.SaveAsync();
        }

        #endregion

        #region GetAllLabelFromDb

        public IEnumerable<Label> GetAllLabelFromDb()
        {
            var labels = _labelRepository.GetAllEntityFromDb(x => true).ToList();

            return labels;
        }

        #endregion

        #region GetFirstLabelFromDbBySearchAsync

        public async Task<Label> GetFirstLabelFromDbBySearchAsync(int? id)
        {
            var labelFromDb = await _labelRepository.GetFirstEntityFromDbBySearchAsync(u => u.Id == id);

            return labelFromDb;
        }

        #endregion

        #region UpdateLabelAsync

        public async Task UpdateLabelAsync(Label label)
        {
            _labelRepository.Update(label);
            await _labelRepository.SaveAsync();
        }

        #endregion
    }
}
