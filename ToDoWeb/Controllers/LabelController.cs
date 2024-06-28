using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.DomainLayer.Models;
using ToDo.ServiceLayer.IServices;
using ToDo.ServiceLayer;

namespace ToDo.Controllers
{
    [Authorize(Roles = SD.roleAdmin)]
    public class LabelController : Controller
    {
        #region Properties

        private readonly ILabelService _labelService;

        #endregion

        #region Constructor

        public LabelController(ILabelService labelService)
        {
            _labelService = labelService;
        }

        #endregion

        #region Index

        public IActionResult Index()
        {
            var labels = _labelService.GetAllLabelFromDb().ToList();

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
                await _labelService.CreateNewLabelAsync(label);
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

            var labelFromDb = await _labelService.GetFirstLabelFromDbBySearchAsync(id);

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
                await _labelService.UpdateLabelAsync(label);
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

            var labelFromDb = await _labelService.GetFirstLabelFromDbBySearchAsync(id);

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
            var label = await _labelService.GetFirstLabelFromDbBySearchAsync(id);

            if (label == null)
            {
                return NotFound();
            }

            await _labelService.DeleteLabelAsync(label);

            TempData["success"] = "Label Deleted Successfully";

            return RedirectToAction("Index");
        }

        #endregion
    }
}
