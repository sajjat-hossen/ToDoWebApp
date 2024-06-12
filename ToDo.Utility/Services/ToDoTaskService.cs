using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDo.DomainLayer.Models;
using ToDo.RepositoryLayer.IRepository;
using ToDo.ServiceLayer.IServices;

namespace ToDo.ServiceLayer.Services
{
    public class ToDoTaskService : IToDoTaskService
    {
        #region Properties

        private readonly IToDoTaskRepository toDoTaskRepository;
        private readonly ILabelRepository labelRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        #endregion

        #region Constructor

        public ToDoTaskService(IToDoTaskRepository toDoTaskRepository, ILabelRepository labelRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.toDoTaskRepository = toDoTaskRepository;
            this.labelRepository = labelRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region GetAllToDoTaskFromDb

        public IEnumerable<ToDoTask> GetAllToDoTaskFromDb()
        {
            string? logedUserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            IEnumerable<ToDoTask> toDoTasks = toDoTaskRepository.GetAllEntityFromDb(u => u.UserId == logedUserId, includeProperties: "Label").ToList();

            return toDoTasks;
        }

        #endregion

        #region GetAllToDoTaskFromDbBySearchAsync

        public async Task<IEnumerable<ToDoTask>> GetAllToDoTaskFromDbBySearchAsync(string queryTerm)
        {
            string? logedUserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            IEnumerable<ToDoTask> toDoTask = await toDoTaskRepository.GetAllEnitityFromDbBySearchAsync((u => ((u.UserId == logedUserId) && (u.Label.Name.StartsWith(queryTerm) || u.Title.StartsWith(queryTerm) || u.Description.StartsWith(queryTerm) || u.Status.StartsWith(queryTerm) || Convert.ToString(u.Priority) == queryTerm))), includeProperties: "Label");

            return toDoTask;
        }

        #endregion

        #region MakeToDoTaskViewModel

        public ToDoTaskViewModel MakeToDoTaskViewModel(IEnumerable<ToDoTask> toDoTasks, string queryTerm, int currentPage, int pageSize)
        {
            ToDoTaskViewModel viewModel = new ToDoTaskViewModel();
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
            IEnumerable<SelectListItem> labelList = labelRepository.GetAllEntityFromDb(x => true).Select(u => new SelectListItem
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
            string? logedUserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            toDoTask.UserId = logedUserId;
            await toDoTaskRepository.AddAsync(toDoTask);
            await toDoTaskRepository.SaveAsync();
        }

        #endregion

        #region GetFirstToDoTaskFromDbBySearchAsync

        public async Task<ToDoTask> GetFirstToDoTaskFromDbBySearchAsync(int? id)
        {
            string? logedUserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            ToDoTask? toDoTask = await toDoTaskRepository.GetFirstEntityFromDbBySearchAsync(u => u.Id == id && u.UserId == logedUserId);

            return toDoTask;
        }

        #endregion

        #region UpdateToDoTaskAsync

        public async Task UpdateToDoTaskAsync(ToDoTask toDoTask)
        {
            string? logedUserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            toDoTask.UserId = logedUserId;
            toDoTaskRepository.Update(toDoTask);
            await toDoTaskRepository.SaveAsync();
        }

        #endregion

        #region DeleteToDoTaskAsync

        public async Task DeleteToDoTaskAsync(ToDoTask todoTask)
        {
            toDoTaskRepository.Remove(todoTask);
            await toDoTaskRepository.SaveAsync();
        }
        #endregion

        #region GetAllCompletedToDoTaskFromDb

        public IEnumerable<ToDoTask> GetAllCompletedToDoTaskFromDb()
        {
            string? logedUserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<ToDoTask> toDoTaskFromDb = toDoTaskRepository.GetAllEntityFromDb(u => u.Status == "Completed" && u.UserId == logedUserId);

            return toDoTaskFromDb;
        }

        #endregion

        #region DeleteRangeToDoTaskAsync

        public async Task DeleteRangeToDoTaskAsync(IEnumerable<ToDoTask> toDoTaskFromDb)
        {
            toDoTaskRepository.RemoveRange(toDoTaskFromDb);
            await toDoTaskRepository.SaveAsync();
        }

        #endregion
    }
}
