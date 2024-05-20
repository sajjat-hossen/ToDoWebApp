using Microsoft.AspNetCore.Mvc;
using ToDoWeb.Data;
using ToDoWeb.Models;

namespace ToDoWeb.Controllers
{
    public class LabelController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public LabelController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<Label> labels = dbContext.Labels.ToList();

            return View(labels);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Label label)
        {
            if (ModelState.IsValid)
            {
                dbContext.Labels.Add(label);
                dbContext.SaveChanges();
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

            Label? labelFromDb = dbContext.Labels.Find(id);

            if (labelFromDb == null)
            {
                return NotFound();
            }

            return View(labelFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Label label)
        {
            if (ModelState.IsValid)
            {
                dbContext.Labels.Update(label);
                dbContext.SaveChanges();
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

            Label? labelFromDb = dbContext.Labels.Find(id);

            if (labelFromDb == null)
            {
                return NotFound();
            }

            return View(labelFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Label? label = dbContext.Labels.Find(id);
            if (label == null)
            {
                return NotFound();
            }

            dbContext.Labels.Remove(label);
            dbContext.SaveChanges();
            TempData["success"] = "Label Deleted Successfully";

            return RedirectToAction("Index");
        }
    }
}
