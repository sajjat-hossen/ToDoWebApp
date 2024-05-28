using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.DataAccess.Data;
using ToDo.DataAccess.Repository.IRepository;
using ToDo.Models;
using ToDo.Utility;

namespace ToDo.Controllers
{
    [Authorize(Roles = SD.roleAdmin)]
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
            List<Label> labels = labelRepo.GetAllEntityFromDb().ToList();

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
        public async Task<IActionResult> Create(Label label)
        {
            if (ModelState.IsValid)
            {
                await labelRepo.AddAsync(label);
                await labelRepo.SaveAsync();
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

            Label? labelFromDb = await labelRepo.GetFirstEntityFromDbBySearchAsync(u => u.Id == id);

            if (labelFromDb == null)
            {
                return NotFound();
            }

            return View(labelFromDb);
        }

        #endregion

        #region Edit

        [HttpPost]
        public async Task<IActionResult> Edit(Label label)
        {
            if (ModelState.IsValid)
            {
                labelRepo.Update(label);
                await labelRepo.SaveAsync();
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

            Label? labelFromDb = await labelRepo.GetFirstEntityFromDbBySearchAsync(u => u.Id == id);

            if (labelFromDb == null)
            {
                return NotFound();
            }

            return View(labelFromDb);
        }

        #endregion

        #region DeletePost

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            Label? label = await labelRepo.GetFirstEntityFromDbBySearchAsync(u => u.Id == id);
            if (label == null)
            {
                return NotFound();
            }

            labelRepo.Remove(label);
            await labelRepo.SaveAsync();
            TempData["success"] = "Label Deleted Successfully";

            return RedirectToAction("Index");
        }

        #endregion
    }
}
