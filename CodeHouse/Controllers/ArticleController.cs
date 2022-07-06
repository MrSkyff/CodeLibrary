using CodeHouse.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectHouse.Models.Article;
using ProjectHouse.ViewModels.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeHouse.Controllers
{

    public class ArticleController : Controller
    {

        public UserContext dbContext;
        public ArticleController(UserContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index() => View(dbContext.Articles.OrderByDescending(n => n.Id));
        
        public IActionResult Read(int id)
        {
            ViewBag.ArtcilePartials = dbContext.ArticlePartials.Where(x => x.ArticleId == id).ToList();
            return View(dbContext.Articles.FirstOrDefault(p => p.Id == id));
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //var model = dbContext.Articles.FirstOrDefault(c => c.Id == id);
            //Article model = dbContext.Articles.Include(x => x.ArticleCategories).ThenInclude(x => x.Category).FirstOrDefault(a=>a.Id == id);

            ArticleViewModel newModel = new ArticleViewModel();

            newModel.Article = dbContext.Articles.Include(x => x.ArticleCategories).ThenInclude(x => x.Category).FirstOrDefault(a => a.Id == id);
            newModel.ArticlePartial = dbContext.ArticlePartials.Where(x => x.ArticleId == id).ToList();

            AssignedCategoryData(newModel.Article);

            return View(newModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(
            int id,
            ArticleViewModel articleViewModel,
            string[] selectedCategories,
            string returnUrl,
            string[] partialTitle,
            string[] partialContent,
            string[] newPartialTitle,
            string[] newPartialContent,
            int[] deletePartialArticle)
        {
            articleViewModel.Article = await dbContext.Articles.Include(x => x.ArticleCategories).ThenInclude(x => x.Category).FirstOrDefaultAsync(c => c.Id == id);

            await TryUpdateModelAsync<ArticleViewModel>(articleViewModel);
            UpdateArticleCategories(selectedCategories, articleViewModel.Article);
            UpdatePartialContent(id, partialTitle, partialContent, articleViewModel);

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes!");
            }

            if (newPartialTitle != null && newPartialContent != null)
            { AddNewPartialPage(id, newPartialTitle, newPartialContent); }
            if (deletePartialArticle != null)
            { DeletePartialContent(id, deletePartialArticle); }

            if (!string.IsNullOrEmpty(returnUrl)) { return LocalRedirect(returnUrl); }
            return RedirectToAction("Index");
        }

        public void AddNewPartialPage(int id, string[] title, string[] content)
        {
            List<ArticlePartial> model = new List<ArticlePartial>();
            for (int i = 0; i < title.Length; i++)
            { model.Add(new ArticlePartial { Title = title[i], ContentText = content[i], ArticleId = id }); }
            dbContext.AddRange(model);
            dbContext.SaveChanges();
        }

        public void DeletePartialContent(int id, int[] deleteIds)
        {
            List<ArticlePartial> modelList = dbContext.ArticlePartials.Where(x => x.ArticleId == id).ToList();
            List<ArticlePartial> modelToDelete = new List<ArticlePartial>();
            for (int i = 0; i < deleteIds.Length; i++)
            {
                var rm = modelList.Find(x => x.Id == deleteIds[i]);
                modelToDelete.Add(rm);
            }
            dbContext.RemoveRange(modelToDelete);
            dbContext.SaveChanges();
        }

        public void UpdatePartialContent(int id, string[] title, string[] content, ArticleViewModel model)
        {

            model.ArticlePartial = dbContext.ArticlePartials.Where(x => x.ArticleId == id).ToList();
            int i = 0;
            foreach (var item in model.ArticlePartial)
            {
                model.ArticlePartial[i].Title = title[i];
                model.ArticlePartial[i].ContentText = content[i];
                i++;
            }
        }

        public void AssignedCategoryData(Article model)
        {
            var allCategories = dbContext.Categories;
            var articleCategories = new HashSet<int>(model.ArticleCategories.Select(c => c.CategoryId));
            var viewModel = new List<CategorySortData>();
            foreach (var category in allCategories)
            {
                viewModel.Add(new CategorySortData
                {
                    CategoryId = category.Id,
                    CategoryName = category.Name,
                    ParentCategoryId = category.ParentId,
                    IsAssigned = articleCategories.Contains(category.Id)
                });
            }
            ViewData["Categories"] = viewModel;
        }

        public void UpdateArticleCategories(string[] selectedCategories, Article model)
        {
            if (selectedCategories == null)
            {
            }
            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var articleCategoriesHS = new HashSet<int>(model.ArticleCategories.Select(c => c.Category.Id));
            foreach (var category in dbContext.Categories)
            {
                if (selectedCategoriesHS.Contains(category.Id.ToString()))
                {
                    if (!articleCategoriesHS.Contains(category.Id))
                    {
                        model.ArticleCategories.Add(new ArticleCategory { ArticleId = model.Id, CategoryId = category.Id });
                    }
                }
                else
                {
                    if (articleCategoriesHS.Contains(category.Id))
                    {
                        ArticleCategory categoryToRemove = model.ArticleCategories.FirstOrDefault(c => c.CategoryId == category.Id);
                        dbContext.Remove(categoryToRemove);
                    }
                }
            }
        }
    }
}
