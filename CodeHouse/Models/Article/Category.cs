using ProjectHouse.Models.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHouse.Models.Article
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Parent { get; set; }
        public int? ParentId { get; set; }
        public ICollection<ArticleCategory> ArticleCategories { get; set;}  
    }
}
