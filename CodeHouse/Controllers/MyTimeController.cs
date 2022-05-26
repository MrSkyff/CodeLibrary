using CodeHouse.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectHouse.Models.MyTime;
using ProjectHouse.ViewModels.MyTime;
using System;
using System.Linq;

namespace ProjectHouse.Controllers
{
    public class MyTimeController : Controller
    {
        protected readonly UserContext dbContext;

        public MyTimeController(UserContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IndexListViewModel model = new IndexListViewModel();
            model.ProjectList = dbContext.Projects.ToList();
            model.TimeRecordList = dbContext.TimeRecords.OrderByDescending(x => x.Id).ToList();

            return View(model);
        }

        public IActionResult ProjectView(int id) => View(dbContext.TimeRecords.Where(x=>x.ProjectId == id).OrderByDescending(x=>x.Id).ToList());
        

        public IActionResult NewTimer()
        {
            ViewData["Projects"] = new SelectList(dbContext.Projects.ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult RunningTimer(TimeRecord model)
        {
            if (model.Id == 0)
            {
                model.StartTime = System.DateTime.Now;
                model.EndTime = null;
                dbContext.Add(model);
            }
            else
            {
                model.EndTime = System.DateTime.Now;
                dbContext.Update(model);
            }
            dbContext.SaveChanges();
            return RedirectToRoute(new { controller = "MyTime", action = "RunningTimer", id = model.Id });
        }
        [HttpGet]
        public IActionResult RunningTimer(int id)
        {
            TimeRecord model = dbContext.TimeRecords.FirstOrDefault(x => x.Id == id);
            return View(model);
        }
        public IActionResult TimeTable()
        {
            return View();
        }
    }
}
