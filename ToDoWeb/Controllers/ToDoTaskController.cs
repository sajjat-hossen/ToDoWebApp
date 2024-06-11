using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDo.DataAccess.Repository.IRepository;
using ToDo.DomainLayer.Models;
using ToDo.Utility;

namespace ToDoWeb.Controllers
{
    [Authorize(Roles = SD.roleUser)]
    public class ToDoTaskController : Controller
    {

        #region Properties
        private readonly IToDoTaskRepository toDoTaskRepo;
        private readonly ILabelRepository labelRepo;
        private readonly UserManager<IdentityUser> userManager;

        #endregion

        #region CTOR

        public ToDoTaskController(IToDoTaskRepository toDoTaskRepo, ILabelRepository labelRepo, UserManager<IdentityUser> userManager)
        {
            this.toDoTaskRepo = toDoTaskRepo;
            this.labelRepo = labelRepo;
            this.userManager = userManager;
        }

        #endregion

        #region Index

        public async Task<IActionResult> Index(string queryTerm = "", int currentPage = 1, int pageSize = 5)
        {
            string logedUserId = userManager.GetUserId(HttpContext.User);
            if (string.IsNullOrEmpty(queryTerm))
            {
                IEnumerable<ToDoTask> toDoTasks = toDoTaskRepo.GetAllEntityFromDb(u=> u.UserId == logedUserId, includeProperties: "Label").ToList();
                ToDoTaskViewModel viewModel = new ToDoTaskViewModel();
                viewModel.PageSize = pageSize;
                viewModel.CurrentPage = currentPage;
                viewModel.QueryTerm = queryTerm;
                viewModel.TotalTasks = toDoTasks.Count();
                viewModel.TotalPages = (int)Math.Ceiling(viewModel.TotalTasks / (double)viewModel.PageSize);
                viewModel.StartTaskNumber = ((viewModel.CurrentPage - 1) * viewModel.PageSize) + 1;
                viewModel.EndTaskNumber = Math.Min((viewModel.StartTaskNumber + viewModel.PageSize - 1), viewModel.TotalTasks);
                viewModel.ToDoTasks = toDoTasks.Skip((viewModel.CurrentPage - 1) * viewModel.PageSize).Take(viewModel.PageSize);
                
                return View(viewModel);
            }

            IEnumerable<ToDoTask> toDoTask = await toDoTaskRepo.GetAllEnitityFromDbBySearchAsync((u => ((u.UserId == logedUserId) && (u.Label.Name.StartsWith(queryTerm) || u.Title.StartsWith(queryTerm) || u.Description.StartsWith(queryTerm) || u.Status.StartsWith(queryTerm) || Convert.ToString(u.Priority) == queryTerm))), includeProperties: "Label");

            ToDoTaskViewModel viewModel1 = new ToDoTaskViewModel();
            viewModel1.PageSize = pageSize;
            viewModel1.CurrentPage = currentPage;
            viewModel1.QueryTerm = queryTerm;
            viewModel1.TotalTasks = toDoTask.Count();
            viewModel1.TotalPages = (int)Math.Ceiling(viewModel1.TotalTasks / (double)viewModel1.PageSize);
            viewModel1.StartTaskNumber = ((viewModel1.CurrentPage - 1) * viewModel1.PageSize) + 1;
            viewModel1.EndTaskNumber = Math.Min((viewModel1.StartTaskNumber + viewModel1.PageSize - 1), viewModel1.TotalTasks);
            viewModel1.ToDoTasks = toDoTask.Skip((viewModel1.CurrentPage - 1) * viewModel1.PageSize).Take(viewModel1.PageSize);
            
            return View(viewModel1);
        }

        #endregion

        #region Create

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> labelList = labelRepo.GetAllEntityFromDb(x => true).Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewBag.LabelList = labelList;

            return View();
        }

        #endregion

        #region Create

        [HttpPost]
        public async Task<IActionResult> Create(ToDoTask toDoTask)
        {
            var logedUserId = userManager.GetUserId(HttpContext.User);
            toDoTask.UserId = logedUserId;

            if (ModelState.IsValid)
            {
                await toDoTaskRepo.AddAsync(toDoTask);
                await toDoTaskRepo.SaveAsync();
                TempData["success"] = "Label Created Successfully";

                return RedirectToAction("Index");
            }

            return View();
        }

        #endregion

        #region Edit

        public async Task<IActionResult> Edit(int? id)
        {
            string logedUserId = userManager.GetUserId(HttpContext.User);
            if (id == null || id == 0)
            {
                return NotFound();
            }

            ToDoTask? toDoTaskFromDb = await toDoTaskRepo.GetFirstEntityFromDbBySearchAsync(u => u.Id == id && u.UserId == logedUserId);

            if (toDoTaskFromDb == null)
            {
                return NotFound();
            }
            IEnumerable<SelectListItem> labelList = labelRepo.GetAllEntityFromDb(x => true).Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewBag.LabelList = labelList;

            return View(toDoTaskFromDb);
        }

        #endregion

        #region Edit

        [HttpPost]
        public async Task<IActionResult> Edit(ToDoTask toDoTask)
        {
            string logedUserId = userManager.GetUserId(HttpContext.User);
            toDoTask.UserId = logedUserId;
            if (ModelState.IsValid)
            {
                toDoTaskRepo.Update(toDoTask);
                await toDoTaskRepo.SaveAsync();
                TempData["success"] = "Label Updated Successfully";

                return RedirectToAction("Index");
            }

            return View();
        }

        #endregion

        #region Delete

        public async Task<IActionResult> Delete(int? id)
        {
            string logedUserId = userManager.GetUserId(HttpContext.User);
            if (id == null || id == 0)
            {
                return NotFound();
            }

            ToDoTask? toDoTaskFromDb = await toDoTaskRepo.GetFirstEntityFromDbBySearchAsync(u => u.Id == id && u.UserId == logedUserId, includeProperties: "Label");

            if (toDoTaskFromDb == null)
            {
                return NotFound();
            }

            return View(toDoTaskFromDb);
        }

        #endregion

        #region DeletePost

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            string logedUserId = userManager.GetUserId(HttpContext.User);
            ToDoTask? toDoTaskFromDb = await toDoTaskRepo.GetFirstEntityFromDbBySearchAsync(u => u.Id == id && u.UserId == logedUserId);
            if (toDoTaskFromDb == null)
            {
                return NotFound();
            }

            toDoTaskRepo.Remove(toDoTaskFromDb);
            await toDoTaskRepo.SaveAsync();
            TempData["success"] = "Label Deleted Successfully";

            return RedirectToAction("Index");
        }

        #endregion

        #region Complete

        public async Task<IActionResult> Complete(int? id)
        {
            string logedUserId = userManager.GetUserId(HttpContext.User);
            if (id == null || id == 0)
            {
                return NotFound();
            }

            ToDoTask? toDoTaskFromDb = await toDoTaskRepo.GetFirstEntityFromDbBySearchAsync(u => u.Id == id && u.UserId == logedUserId);

            if (toDoTaskFromDb == null)
            {
                return NotFound();
            }

            toDoTaskFromDb.Status = "Completed";

            toDoTaskRepo.Update(toDoTaskFromDb);
            await toDoTaskRepo.SaveAsync();
            TempData["success"] = "Congratulations, you have completed task successfully";

            return RedirectToAction("Index");

        }

        #endregion

        #region DeleteCompletedTask
        public async Task<IActionResult> DeleteCompletedTask()
        {
            string logedUserId = userManager.GetUserId(HttpContext.User);
            IEnumerable<ToDoTask> toDoTaskFromDb = toDoTaskRepo.GetAllEntityFromDb(u => u.Status == "Completed" && u.UserId == logedUserId);

            toDoTaskRepo.RemoveRange(toDoTaskFromDb);
            await toDoTaskRepo.SaveAsync();

            TempData["success"] = "Removed All The Completed Task";

            return RedirectToAction("Index");

        }
        #endregion

    }
}
