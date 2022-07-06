using CodeHouse.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectHouse.Models.Learning;
using System;
using System.Linq;

namespace ProjectHouse.Controllers
{

    public class LearningController : Controller
    {
        private readonly UserContext dbContext;

        public LearningController(UserContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            ViewBag.HomeWorks = dbContext.HomeWorks.ToList();
            return View(dbContext.Groupes.ToList().OrderByDescending(o => o.Position));
        }
        [HttpGet]
        public IActionResult AddEditGroupe(int? id)
        {
            if (id == null) { return View(); }
            return View(dbContext.Groupes.FirstOrDefault(g => g.Id == id));
        }
        [HttpPost]
        public IActionResult AddEditGroupe(Groupe model, string returnUrl)
        {
            if (model.Id == 0)
            {
                model.Date = DateTime.Now;
                model.Position = 0;
                model.Status = GroupeStatus.NotCompleted;
                dbContext.Groupes.Add(model);
            }
            else
            {
                dbContext.Groupes.Update(model);
            }
            dbContext.SaveChanges();
            if (!String.IsNullOrEmpty(returnUrl)) { return LocalRedirect(returnUrl); }
            return RedirectToAction("Index");
        }
        public IActionResult HomeWorkByGroupe(int? id)
        {
            if (id == null) { return RedirectToAction("Index"); }
            ViewBag.GroupName = dbContext.Groupes.FirstOrDefault(i=>i.Id == id).Name;
            return View(dbContext.HomeWorks.ToList().Where(g => g.GroupeId == id).OrderBy(s => s.Status).ThenByDescending(p=>p.Position));
        }
        [HttpGet]
        public IActionResult HomeWork(int id) => View(dbContext.HomeWorks.FirstOrDefault(i=>i.Id == id));
        [HttpGet]
        public IActionResult AddEditHomeWork(int id)
        {
            ViewBag.Groupes = new SelectList(dbContext.Groupes.ToList(), "Id", "Name").OrderByDescending(i => i.Value);
            return View(dbContext.HomeWorks.FirstOrDefault(i => i.Id == id));
        }
        [HttpPost]
        public IActionResult AddEditHomeWork(HomeWork model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                dbContext.HomeWorks.Update(model);
            }
            else
            {
                model.Date = DateTime.Now;
                model.Status = HomeWorkStatus.NotStarted;
                dbContext.HomeWorks.Add(model);
            }
            dbContext.SaveChanges();
            if (!String.IsNullOrEmpty(returnUrl)) { return LocalRedirect(returnUrl); }
            return RedirectToAction("Index");
        }
    }
}
