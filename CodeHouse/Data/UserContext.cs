using CodeHouse.Models.Account;
using ProjectHouse.Models.Article;
using Microsoft.EntityFrameworkCore;
using ProjectHouse.Models.Learning;
using ProjectHouse.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectHouse.Models.MyTime;

namespace CodeHouse.Data
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<ArticlePartial> ArticlePartials { get; set; }
        public DbSet<Groupe> Groupes { get; set; }
        public DbSet<HomeWork> HomeWorks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectArticle> ProjectArticles { get; set; }
        public DbSet<TaskCategory> ProjectsTasksCategories { get; set; }
        public DbSet<ProjectHouse.Models.Project.Task> ProjectsTasks { get; set; }
        public DbSet<TimeRecord> TimeRecords { get; set; }
             
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Role>().HasData(
            //    new Role { Id = 1, Name = "admin" },
            //    new Role { Id = 2, Name = "user" }
            //    );
            //modelBuilder.Entity<User>().HasData(
            //    new User { Id = 1, Email = "admin@mail.ru", Password = "12345", RoleId = 1 },
            //    new User { Id = 2, Email = "user@mail.ru", Password = "12345", RoleId = 2 }
            //    );
            //modelBuilder.Entity<ArticleCategoryModel>().HasData(
            //     new ArticleCategoryModel { Id = 1, ParentCategoryId = null, Name = "[C# .NET] Code", Description = "Code" },
            //     new ArticleCategoryModel { Id = 2, ParentCategoryId = null, Name = "[C# ASP.NET] Code", Description = "Code" },
            //     new ArticleCategoryModel { Id = 3, ParentCategoryId = 1, Name = "[C# .NET] Samples", Description = "Code" },
            //     new ArticleCategoryModel { Id = 4, ParentCategoryId = 2, Name = "[C# ASP.NET] Samples", Description = "Code" }
            //    );
            //modelBuilder.Entity<ArticleModel>().HasData(
            //    new ArticleModel { Id = 1, CategoryId = 1, Title = "C# Classes", Text = "text", ShortText = "tialize передается контекст данных. И если данные в таблице Phones в бд отсутствуют (if (!context.Phones.Any())), то добавляются три объекта. Чтобы инициализатор базы данных вызывался при старте приложения, изменим класс Program следующим образом" },
            //    new ArticleModel { Id = 7, CategoryId = 3, Title = "Инициализация БД ролями и пользователями", Text = "text", ShortText = "Данный класс определяет один статический метод Initialize(), в котором происходит добавление трех начальных элементов - объектов Phone. Для добавления объектов в бд в метод Initialize передается контекст данных. И если данные в таблице Phones в бд отсутствуют (if (!context.Phones.Any())), то добавляются три объекта. Чтобы инициализатор базы данных вызывался при старте приложения, изменим класс Program следующим образом" }
            //    );
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Article>().ToTable("Articles");
            //modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<ArticleCategory>().HasKey(key => new { key.ArticleId, key.CategoryId });

            modelBuilder.Entity<ArticleCategory>()
                .HasOne<Article>(c => c.Article)
                .WithMany(c => c.ArticleCategories)
                .HasForeignKey(c => c.ArticleId);

            modelBuilder.Entity<ArticleCategory>()
                .HasOne(c => c.Category)
                .WithMany(c => c.ArticleCategories)
                .HasForeignKey(k => k.CategoryId);
                
        }
    }

}
