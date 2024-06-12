using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDo.DomainLayer.Models;
using ToDo.RepositoryLayer.IRepository;
using ToDo.ServiceLayer.IServices;
using ToDo.ServiceLayer;

namespace ToDoWeb.Controllers
{
    [Authorize(Roles = SD.roleUser)]
    public class ToDoTaskController : Controller
    {
        #region Properties

        private readonly IToDoTaskService toDoTaskService;

        #endregion

        #region Constructor

        public ToDoTaskController(IToDoTaskService toDoTaskService)
        {
            this.toDoTaskService = toDoTaskService;
        }

        #endregion

        #region Index

        public async Task<IActionResult> Index(string queryTerm = "", int currentPage = 1, int pageSize = 5)
        {
            ToDoTaskViewModel toDoTaskViewModel = await toDoTaskService.GetDoTaskViewModelFromDbAsync(queryTerm, currentPage, pageSize);
            
            return View(toDoTaskViewModel);
        }

        #endregion

        #region Create

        public IActionResult Create()
        {
            ViewBag.LabelList = toDoTaskService.GetLabelList();

            return View();
        }

        #endregion

        #region Create

        [HttpPost]
        public async Task<IActionResult> Create(ToDoTask toDoTask)
        {
            if (ModelState.IsValid)
            {
                await toDoTaskService.CreateNewToDoTaskAsync(toDoTask);
                TempData["success"] = "Label Created Successfully";

                return RedirectToAction("Index");
            }

            return View();
        }

        #endregion

        #region Edit

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            ToDoTask? toDoTaskFromDb = await toDoTaskService.GetFirstToDoTaskFromDbBySearchAsync(id);

            if (toDoTaskFromDb == null)
            {
                return NotFound();
            }

            ViewBag.LabelList = toDoTaskService.GetLabelList();

            return View(toDoTaskFromDb);
        }

        #endregion

        #region Edit

        [HttpPost]
        public async Task<IActionResult> Edit(ToDoTask toDoTask)
        {
            if (ModelState.IsValid)
            {
                await toDoTaskService.UpdateToDoTaskAsync(toDoTask);
                TempData["success"] = "Label Updated Successfully";

                return RedirectToAction("Index");
            }

            return View();
        }

        #endregion

        #region Delete

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            ToDoTask? toDoTaskFromDb = await toDoTaskService.GetFirstToDoTaskFromDbBySearchAsync(id);

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
            ToDoTask? toDoTaskFromDb = await toDoTaskService.GetFirstToDoTaskFromDbBySearchAsync(id);

            if (toDoTaskFromDb == null)
            {
                return NotFound();
            }

            await toDoTaskService.DeleteToDoTaskAsync(toDoTaskFromDb);
            TempData["success"] = "Label Deleted Successfully";

            return RedirectToAction("Index");
        }

        #endregion

        #region Complete

        public async Task<IActionResult> Complete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            ToDoTask? toDoTaskFromDb = await toDoTaskService.GetFirstToDoTaskFromDbBySearchAsync(id);

            if (toDoTaskFromDb == null)
            {
                return NotFound();
            }

            toDoTaskFromDb.Status = "Completed";

            await toDoTaskService.UpdateToDoTaskAsync(toDoTaskFromDb);
            TempData["success"] = "Congratulations, you have completed task successfully";

            return RedirectToAction("Index");

        }

        #endregion

        #region DeleteCompletedTask
        public async Task<IActionResult> DeleteCompletedTask()
        {
            IEnumerable<ToDoTask> toDoTaskFromDb = toDoTaskService.GetAllCompletedToDoTaskFromDb();
            await toDoTaskService.DeleteRangeToDoTaskAsync(toDoTaskFromDb);

            TempData["success"] = "Removed All The Completed Task";

            return RedirectToAction("Index");

        }
        #endregion
    }
}
