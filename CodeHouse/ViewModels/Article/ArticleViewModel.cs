using ProjectHouse.Models.Article;
using System.Collections.Generic;

namespace ProjectHouse.ViewModels.Article
{
    public class ArticleViewModel
    {
        public Models.Article.Article Article { get; set; }
        public List<ArticlePartial> ArticlePartial { get; set; }
    }
}
