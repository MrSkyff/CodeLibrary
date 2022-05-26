using System.Collections.Generic;

namespace ProjectHouse.ViewModels.Project
{
    public class ProjectListViewModel
    {
        public IEnumerable<Models.Project.Project> Projects { get; set; }
        public IEnumerable<Models.Project.Task> Tasks { get; set; }
    }
}
