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
            return View();
        }
    }
}
