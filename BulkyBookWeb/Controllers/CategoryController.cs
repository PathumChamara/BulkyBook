using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CategoryController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _applicationDbContext.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                //ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name");
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _applicationDbContext.Categories.AddAsync(category);
                _applicationDbContext.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return View(category);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null ||id == 0)
            {
                return NotFound();
            }
            var category = _applicationDbContext.Categories.Find(id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                //ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name");
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _applicationDbContext.Categories.Update(category);
                _applicationDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            else
            {
                var category = _applicationDbContext.Categories.FirstOrDefault(x => x.Id == id);
                if (category!= null)
                {
                    _applicationDbContext.Categories.Remove(category);
                    _applicationDbContext.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
        }

    }
}
