using Microsoft.AspNetCore.Mvc;
using ToDo.DataAccess.Repository.IRepository;
using ToDo.Models;

namespace ToDoWeb.Controllers
{
    public class ToDoTaskController : Controller
    {
        private readonly IToDoTaskRepository toDoTaskRepo;

        public ToDoTaskController(IToDoTaskRepository toDoTaskRepo)
        {
            this.toDoTaskRepo = toDoTaskRepo;
        }
        public IActionResult Index()
        {
            List<ToDoTask> toDoTasks= toDoTaskRepo.GetAll().ToList();
            return View(toDoTasks);
        }
        public IActionResult Create()
        {
            return View();
        }

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

            return View(toDoTaskFromDb);
        }

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

        public IActionResult Delete(int? id)
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

            return View(toDoTaskFromDb);
        }

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
    }
}
