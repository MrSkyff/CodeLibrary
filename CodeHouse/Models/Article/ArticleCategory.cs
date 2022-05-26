using ProjectHouse.Models.Article;

namespace ProjectHouse.Models.Article
{
    public class ArticleCategory
    {
        public int ArticleId { get; set; }
        public int CategoryId { get; set; }
        public ProjectHouse.Models.Article.Article Article { get; set; }
        public Category Category { get; set; }
    }
}
