using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class ApplicationTypeController : Controller
    {
        AppDbContext _context;

        public ApplicationTypeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<ApplicationType> types = _context.ApplicationTypes;
            return View(types);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType type)
        {
            if (ModelState.IsValid)
            {
                _context.ApplicationTypes.Add(type);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(type);
        }

        public IActionResult Edit(int? id)
        {
            if (id==0|| id==null)
            {
                return NotFound();
            }
            ApplicationType? type = _context.ApplicationTypes.Find(id);
            if (type == null)
            {
                return NotFound();
            }
            return View(type);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationType type)
        {
            if (ModelState.IsValid)
            {
                _context.ApplicationTypes.Update(type);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(type);
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            ApplicationType? type = _context.ApplicationTypes.Find(id);
            if (type == null)
            {
                return NotFound();
            }
            return View(type);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            ApplicationType? type = _context.ApplicationTypes.Find(id);
            if (type==null) {
                return NotFound();
            }
            _context.ApplicationTypes.Remove(type);
            _context.SaveChanges();
            return RedirectToAction("Index");
            
        }



       

    }
}
