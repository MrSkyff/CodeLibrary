namespace ProjectHouse.Models.Project
{
    public class ProjectArticle
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
