using ProjectHouse.Models.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHouse.Models.Article
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string ShortText { get; set; }
        public DateTime Added { get; set; }
        public DateTime LastUpdate { get; set; }
        public ICollection<ArticleCategory> ArticleCategories { get; set; }
        public ICollection<ArticlePartial> ArticlePartials { get; set; }
    }
}
