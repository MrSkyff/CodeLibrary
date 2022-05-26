using ProjectHouse.Models.Project;
using System.Collections.Generic;

namespace ProjectHouse.ViewModels.Project
{
    public class ProjectMainViewModel
    {
        public Models.Project.Project Project { get; set; }
        public IEnumerable<Task> ProjectTaskList { get; set; }
        public IEnumerable<ProjectArticle> ProjectArticleList { get; set; }

    }
}
