namespace ProjectHouse.Models.Article
{
    public class ArticlePartial
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ContentText { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
