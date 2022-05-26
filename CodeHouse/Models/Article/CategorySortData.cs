namespace ProjectHouse.Models.Article
{
    public class CategorySortData
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsAssigned { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
