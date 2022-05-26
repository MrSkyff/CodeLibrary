using CodeHouse.Data;
using ProjectHouse.Models.Article;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CodeHouse.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        public UserContext DbContext;
        public AdminController(UserContext DbContext)
        {
            this.DbContext = DbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult EditArticle(int id)
        {
            ViewBag.Categories = DbContext.Categories.ToArray();
            return View(DbContext.Articles.Single(a => a.Id == id));
        }
        [HttpPost]
        public IActionResult EditArticle(Article model)
        {
            DbContext.Articles.Update(model);
            DbContext.SaveChanges();
           return RedirectToAction("Index");
        }
        public IActionResult AddArticle()
        {
            ViewBag.Categories = DbContext.Categories.ToArray();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddArticle(Article model)
        {
            DbContext.Articles.Add(model);
            await DbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult DeleteArticle()
        {
            return View();
        }
        public IActionResult AddCategory()
        {
            ViewBag.Categories = DbContext.Categories.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Category model)
        {
            DbContext.Add(model);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            ViewBag.Categories = DbContext.Categories.ToList();
            return View(DbContext.Categories.Single(c => c.Id == id));
        }
        [HttpPost]
        public IActionResult EditCategory(Category model)
        {
            DbContext.Categories.Update(model);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            
            return View(DbContext.Categories.Single(c => c.Id == id));
        }
        [HttpPost]
        public IActionResult DeleteCategory(Category model)
        {
            DbContext.Categories.Remove(model);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
