using CodeHouse.Data;
using ProjectHouse.Models.Article;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ProjectHouse.Controllers
{
    public class CategoryController : Controller
    {
        public UserContext dbContext { get; set; }
        public CategoryController(UserContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Categories()
        {
            return View(dbContext.Categories.ToList());
        }
        [HttpGet]
        public IActionResult Category(int? id)
        {
            var model = dbContext.Articles.Include(c=>c.ArticleCategories).Where(c=>c.ArticleCategories.Any(b=>b.CategoryId == id)).ToList();
            ViewBag.CategoryId = dbContext.Categories.FirstOrDefault(x => x.Id == id).Id;
            ViewBag.CategoryName = dbContext.Categories.FirstOrDefault(x => x.Id == id).Name;
            return View(model);
        }
        [HttpGet]
        public IActionResult AddEdit(int? id)
        {
            var model = dbContext.Categories.FirstOrDefault(x=>x.Id == id);
            ViewBag.Categories = dbContext.Categories.ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult AddEdit(Category model, string returnUrl)
        {
            dbContext.Update(model);
            dbContext.SaveChanges();
            if (!string.IsNullOrEmpty(returnUrl)) { return LocalRedirect(returnUrl); }
            return RedirectToAction("Categories");
        }
    }
}
