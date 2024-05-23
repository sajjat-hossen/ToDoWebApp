using Microsoft.AspNetCore.Mvc;
using ToDo.DataAccess.Data;
using ToDo.DataAccess.Repository.IRepository;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class LabelController : Controller
    {

        #region Properties
        private readonly ILabelRepository labelRepo;

        #endregion

        #region CTOR

        public LabelController(ILabelRepository labelRepo)
        {
            this.labelRepo = labelRepo;
        }

        #endregion

        #region Index

        public IActionResult Index()
        {
            List<Label> labels = labelRepo.GetAll().ToList();

            return View(labels);
        }

        #endregion

        #region Create

        public IActionResult Create()
        {
            return View();
        }

        #endregion

        #region Create

        [HttpPost]
        public IActionResult Create(Label label)
        {
            if (ModelState.IsValid)
            {
                labelRepo.Add(label);
                labelRepo.Save();
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

            Label? labelFromDb = labelRepo.Get(u => u.Id == id);

            if (labelFromDb == null)
            {
                return NotFound();
            }

            return View(labelFromDb);
        }

        #endregion

        #region Edit

        [HttpPost]
        public IActionResult Edit(Label label)
        {
            if (ModelState.IsValid)
            {
                labelRepo.Update(label);
                labelRepo.Save();
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

            Label? labelFromDb = labelRepo.Get(u => u.Id == id);

            if (labelFromDb == null)
            {
                return NotFound();
            }

            return View(labelFromDb);
        }

        #endregion

        #region DeletePost

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Label? label = labelRepo.Get(u => u.Id == id);
            if (label == null)
            {
                return NotFound();
            }

            labelRepo.Remove(label);
            labelRepo.Save();
            TempData["success"] = "Label Deleted Successfully";

            return RedirectToAction("Index");
        }

        #endregion
    }
}
