using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDo.DataAccess.Repository.IRepository;
using ToDo.Models;

namespace ToDoWeb.Controllers
{
    public class ToDoTaskController : Controller
    {

        #region Properties
        private readonly IToDoTaskRepository toDoTaskRepo;
        private readonly ILabelRepository labelRepo;

        #endregion

        #region CTOR

        public ToDoTaskController(IToDoTaskRepository toDoTaskRepo, ILabelRepository labelRepo)
        {
            this.toDoTaskRepo = toDoTaskRepo;
            this.labelRepo = labelRepo;
        }

        #endregion

        #region Index

        public IActionResult Index()
        {
            List<ToDoTask> toDoTasks= toDoTaskRepo.GetAll(includeProperties: "Label").ToList();
            
            return View(toDoTasks);
        }

        #endregion

        #region Create

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> labelList = labelRepo.GetAll().Select(u => new SelectListItem
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
        public IActionResult Create(ToDoTask toDoTask)
        {
            if (ModelState.IsValid)
            {
                toDoTaskRepo.Add(toDoTask);
                toDoTaskRepo.Save();
                TempData["success"] = "Label Created Successfully";

                return RedirectToAction("Index");
            }

            return View();
        }

        #endregion

        #region Edit

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            ToDoTask? toDoTaskFromDb = toDoTaskRepo.Get(u => u.Id == id);

            if (toDoTaskFromDb == null)
            {
                return NotFound();
            }
            IEnumerable<SelectListItem> labelList = labelRepo.GetAll().Select(u => new SelectListItem
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
        public IActionResult Edit(ToDoTask toDoTask)
        {
            if (ModelState.IsValid)
            {
                toDoTaskRepo.Update(toDoTask);
                toDoTaskRepo.Save();
                TempData["success"] = "Label Updated Successfully";

                return RedirectToAction("Index");
            }

            return View();
        }

        #endregion

        #region Delete

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            ToDoTask? toDoTaskFromDb = toDoTaskRepo.Get(u => u.Id == id, includeProperties: "Label");

            if (toDoTaskFromDb == null)
            {
                return NotFound();
            }

            return View(toDoTaskFromDb);
        }

        #endregion

        #region DeletePost

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            ToDoTask? toDoTaskFromDb = toDoTaskRepo.Get(u => u.Id == id);
            if (toDoTaskFromDb == null)
            {
                return NotFound();
            }

            toDoTaskRepo.Remove(toDoTaskFromDb);
            toDoTaskRepo.Save();
            TempData["success"] = "Label Deleted Successfully";

            return RedirectToAction("Index");
        }

        #endregion

        #region Complete

        public IActionResult Complete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            ToDoTask? toDoTaskFromDb = toDoTaskRepo.Get(u => u.Id == id);

            if (toDoTaskFromDb == null)
            {
                return NotFound();
            }

            toDoTaskFromDb.Status = "Completed";

            toDoTaskRepo.Update(toDoTaskFromDb);
            toDoTaskRepo.Save();
            TempData["success"] = "Congratulations, you have completed task successfully";

            return RedirectToAction("Index");

        }

        #endregion

        #region DeleteCompletedTask
        public IActionResult DeleteCompletedTask()
        {

            ToDoTask? toDoTaskFromDb = toDoTaskRepo.Get(u => u.Status == "Completed");

            while (toDoTaskFromDb != null) {
                toDoTaskRepo.Remove(toDoTaskFromDb);
                toDoTaskRepo.Save();
                toDoTaskFromDb = toDoTaskRepo.Get(u => u.Status == "Completed");
            }

            TempData["success"] = "Removed All The Completed Task";

            return RedirectToAction("Index");

        }
        #endregion

    }
}
