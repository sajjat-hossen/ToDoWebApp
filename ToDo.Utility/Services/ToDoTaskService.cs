using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using ToDo.DomainLayer.Models;
using ToDo.RepositoryLayer.IRepository;
using ToDo.ServiceLayer.IServices;

namespace ToDo.ServiceLayer.Services
{
    public class ToDoTaskService : IToDoTaskService
    {
        #region Properties

        private readonly IToDoTaskRepository _toDoTaskRepository;
        private readonly ILabelRepository _labelRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Constructor

        public ToDoTaskService(IToDoTaskRepository toDoTaskRepository, ILabelRepository labelRepository, IHttpContextAccessor httpContextAccessor)
        {
            _toDoTaskRepository = toDoTaskRepository;
            _labelRepository = labelRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region GetAllToDoTaskFromDb

        public IEnumerable<ToDoTask> GetAllToDoTaskFromDb()
        {
            var logedUserId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            var toDoTasks = _toDoTaskRepository.GetAllEntityFromDb(u => u.UserId == logedUserId, includeProperties: "Label").ToList();

            return toDoTasks;
        }

        #endregion

        #region GetAllToDoTaskFromDbBySearchAsync

        public async Task<IEnumerable<ToDoTask>> GetAllToDoTaskFromDbBySearchAsync(string queryTerm)
        {
            var logedUserId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            var toDoTask = await _toDoTaskRepository.GetAllEnitityFromDbBySearchAsync((u => ((u.UserId == logedUserId) && (u.Label.Name.StartsWith(queryTerm) || u.Title.StartsWith(queryTerm) || u.Description.StartsWith(queryTerm) || u.Status.StartsWith(queryTerm) || Convert.ToString(u.Priority) == queryTerm))), includeProperties: "Label");

            return toDoTask;
        }

        #endregion

        #region MakeToDoTaskViewModel

        public ToDoTaskViewModel MakeToDoTaskViewModel(IEnumerable<ToDoTask> toDoTasks, string queryTerm, int currentPage, int pageSize)
        {
            var viewModel = new ToDoTaskViewModel();
            viewModel.PageSize = pageSize;
            viewModel.CurrentPage = currentPage;
            viewModel.QueryTerm = queryTerm;
            viewModel.TotalTasks = toDoTasks.Count();
            viewModel.TotalPages = (int)Math.Ceiling(viewModel.TotalTasks / (double)viewModel.PageSize);
            viewModel.StartTaskNumber = ((viewModel.CurrentPage - 1) * viewModel.PageSize) + 1;
            viewModel.EndTaskNumber = Math.Min((viewModel.StartTaskNumber + viewModel.PageSize - 1), viewModel.TotalTasks);
            viewModel.ToDoTasks = toDoTasks.Skip((viewModel.CurrentPage - 1) * viewModel.PageSize).Take(viewModel.PageSize);

            return viewModel;
        }

        #endregion

        #region GetDoTaskViewModelFromDbAsync

        public async Task<ToDoTaskViewModel> GetDoTaskViewModelFromDbAsync(string queryTerm, int currentPage, int pageSize)
        {
            IEnumerable<ToDoTask> toDoTasks;

            if (string.IsNullOrEmpty(queryTerm))
            {
                toDoTasks = GetAllToDoTaskFromDb().ToList();
            }
            else
            {
                toDoTasks = await GetAllToDoTaskFromDbBySearchAsync(queryTerm);
            }

            return MakeToDoTaskViewModel(toDoTasks, queryTerm, currentPage, pageSize);
        }

        #endregion

        #region GetLabelList

        public IEnumerable<SelectListItem> GetLabelList()
        {
            var labelList = _labelRepository.GetAllEntityFromDb(x => true).Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return labelList;
        }

        #endregion

        #region CreateNewToDoTaskAsync

        public async Task CreateNewToDoTaskAsync(ToDoTask toDoTask)
        {
            var logedUserId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            toDoTask.UserId = logedUserId;

            await _toDoTaskRepository.AddAsync(toDoTask);
            await _toDoTaskRepository.SaveAsync();
        }

        #endregion

        #region GetFirstToDoTaskFromDbBySearchAsync

        public async Task<ToDoTask> GetFirstToDoTaskFromDbBySearchAsync(int? id)
        {
            var logedUserId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            var toDoTask = await _toDoTaskRepository.GetFirstEntityFromDbBySearchAsync(u => u.Id == id && u.UserId == logedUserId);

            return toDoTask;
        }

        #endregion

        #region UpdateToDoTaskAsync

        public async Task UpdateToDoTaskAsync(ToDoTask toDoTask)
        {
            var logedUserId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            toDoTask.UserId = logedUserId;

            _toDoTaskRepository.Update(toDoTask);
            await _toDoTaskRepository.SaveAsync();
        }

        #endregion

        #region DeleteToDoTaskAsync

        public async Task DeleteToDoTaskAsync(ToDoTask todoTask)
        {
            _toDoTaskRepository.Remove(todoTask);
            await _toDoTaskRepository.SaveAsync();
        }
        #endregion

        #region GetAllCompletedToDoTaskFromDb

        public IEnumerable<ToDoTask> GetAllCompletedToDoTaskFromDb()
        {
            var logedUserId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            var toDoTaskFromDb = _toDoTaskRepository.GetAllEntityFromDb(u => u.Status == "Completed" && u.UserId == logedUserId);

            return toDoTaskFromDb;
        }

        #endregion

        #region DeleteRangeToDoTaskAsync

        public async Task DeleteRangeToDoTaskAsync(IEnumerable<ToDoTask> toDoTaskFromDb)
        {
            _toDoTaskRepository.RemoveRange(toDoTaskFromDb);
            await _toDoTaskRepository.SaveAsync();
        }

        #endregion
    }
}
