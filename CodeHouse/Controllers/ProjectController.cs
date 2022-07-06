using CodeHouse.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectHouse.Models.Project;
using System.Linq;
using System;
using ProjectHouse.ViewModels.Project;

namespace ProjectHouse.Controllers
{

    public class ProjectController : Controller
    {
        protected readonly UserContext dbContext;

        public ProjectController(UserContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            ProjectListViewModel model = new ProjectListViewModel();
            model.Projects = dbContext.Projects.OrderByDescending(x => x.Position);
            model.Tasks = dbContext.ProjectsTasks.Where(x => x.TaskStatus != TaskStatus.Completed);
            return View(model);
        }

        [HttpGet]
        public IActionResult AddEditProject(int? id)
        {
            if (id == null) { return View(); }
            return View(dbContext.Projects.FirstOrDefault(p => p.Id == id));
        }
        [HttpPost]
        public IActionResult AddEditProject(Project model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                dbContext.Projects.Update(model);
            }
            else
            {
                dbContext.Projects.Add(model);
            }
            dbContext.SaveChanges();

            if (!string.IsNullOrEmpty(returnUrl)) { return LocalRedirect(returnUrl); }
            return View(model);
        }
        [HttpGet]
        public IActionResult AddEditTask(int? id, int? id2, int id3)
        {
            if (id == null || id == 0 && id2 == null) { return Content("You miss some details!"); }

            if (id == 0)
            {
                Task task = new Task() { ProjectId = id2.Value, CategoryId = id3 };
                return View(task);
            }

            var model = dbContext.ProjectsTasks.Where(x => x.Id == id).FirstOrDefault();

            if (model != null) { return View(model); }

            return Content("Model is null");
        }
        [HttpPost]
        public IActionResult AddEditTask(Task model, string returnUrl)
        {
            if (ModelState.IsValid && model.Id != 0)
            {
                dbContext.ProjectsTasks.Update(model);
            }
            else
            {
                model.TaskImportance = TaskImportance.Normal;
                model.TaskStatus = TaskStatus.InActive;
                model.Date = DateTime.Now;
                dbContext.ProjectsTasks.Add(model);
            }
            dbContext.SaveChanges();

            if (!string.IsNullOrEmpty(returnUrl)) { return LocalRedirect(returnUrl); }
            return View(model);
        }
        [HttpGet]
        public IActionResult AddEditTaskCategory(int? id, int? id2)
        {
            if (id == null || id == 0)
            {
                TaskCategory model = new TaskCategory() { ProjectId = id2.Value };
                return View(model);
            }
            return View(dbContext.ProjectsTasksCategories.Where(x => x.Id == id).FirstOrDefault());
        }
        [HttpPost]
        public IActionResult AddEditTaskCategory(TaskCategory model, string returnUrl)
        {
            if (ModelState.IsValid && model.Id != 0)
            { dbContext.ProjectsTasksCategories.Update(model); }
            else { dbContext.ProjectsTasksCategories.Add(model); }
            dbContext.SaveChanges();
            if (!string.IsNullOrEmpty(returnUrl)) { return LocalRedirect(returnUrl); }
            return View(model);
        }

        public IActionResult ProjectMain(int id)
        {
            ProjectMainViewModel projectMainViewModel = new ProjectMainViewModel();
            projectMainViewModel.ProjectTaskList = dbContext.ProjectsTasks.Where(x => x.ProjectId == id).OrderBy(x => x.TaskStatus).ThenBy(x => x.TaskImportance).ThenByDescending(x => x.Position).ToList();
            projectMainViewModel.ProjectArticleList = dbContext.ProjectArticles.Where(x=>x.ProjectId == id).ToList();
            projectMainViewModel.Project = dbContext.Projects.FirstOrDefault(x => x.Id == id);
            return View(projectMainViewModel);
        }

        public IActionResult TaskCategory(int id)
        {
            ViewBag.ProjectName = dbContext.Projects.Where(x => x.Id == id).FirstOrDefault()?.Name;
            ViewBag.ProjectId = dbContext.Projects.Where(x => x.Id == id).FirstOrDefault()?.Id;
            return View(dbContext.ProjectsTasksCategories.Where(x => x.ProjectId == id).OrderByDescending(x => x.Position).ToList());
        }
        public IActionResult TaskList(int id)
        {
            var model = dbContext.ProjectsTasksCategories.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.ProjectName = dbContext.Projects.Where(x => x.Id == model.ProjectId).FirstOrDefault()?.Name;
            int projectID = dbContext.Projects.Where(x => x.Id == model.ProjectId).FirstOrDefault().Id;
            ViewBag.ProjectId = projectID;
            ViewBag.CategoryId = id;
            ViewBag.CategoryName = dbContext.ProjectsTasksCategories.Where(x => x.Id == id).FirstOrDefault().Name;

            return View(dbContext.ProjectsTasks
                .Where(x => x.CategoryId == id)
                .OrderBy(x => x.TaskStatus)
                .ThenByDescending(x => x.Position)
                .ThenBy(x => x.TaskImportance)
                .ToList());
        }
        public IActionResult TaskView(int id)
        {
            var model = dbContext.ProjectsTasks.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.ProjectName = dbContext.Projects.Where(x => x.Id == model.ProjectId).FirstOrDefault()?.Name;
            ViewBag.ProjectId = dbContext.Projects.Where(x => x.Id == model.ProjectId).FirstOrDefault()?.Id;
            ViewBag.CategoryName = dbContext.ProjectsTasksCategories.Where(x => x.Id == model.CategoryId).FirstOrDefault()?.Name;
            ViewBag.CategoryId = dbContext.ProjectsTasksCategories.Where(x => x.Id == model.CategoryId).FirstOrDefault()?.Id;
            return View(model);
        }
        [HttpGet]
        public IActionResult Article(int id)
        {
           var model = dbContext.ProjectArticles.FirstOrDefault(x => x.Id == id);
           ViewBag.Project = dbContext.Projects.Where(x => x.Id == model.ProjectId).FirstOrDefault();
           return View(model);
        }
        [HttpGet]
        public IActionResult AddEditArticle(int id, int id2)
        {
            if (id2 <= 0) { return RedirectToAction("Index"); }

            if (id > 0) { return View(dbContext.ProjectArticles.FirstOrDefault(x => x.Id == id)); }
            else { ProjectArticle projectArticle = new ProjectArticle() { ProjectId = id2 }; return View(projectArticle); }
        }
        [HttpPost]
        public IActionResult AddEditArticle(ProjectArticle model, string returnUrl)
        {
            if (model.Id > 0) { dbContext.Update(model); }
            else { dbContext.Add(model); }
            dbContext.SaveChanges();

            if(!String.IsNullOrEmpty(returnUrl)) { return LocalRedirect(returnUrl); }
            else { return View(model); }
        }
    }
}
