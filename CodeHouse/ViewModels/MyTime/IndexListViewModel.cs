using ProjectHouse.Models.MyTime;
using ProjectHouse.Models.Project;
using System.Collections.Generic;

namespace ProjectHouse.ViewModels.MyTime
{
    public class IndexListViewModel
    {
        public List<Models.Project.Project> ProjectList { get; set; }
        public IEnumerable<TimeRecord> TimeRecordList { get; set; }
    }
}
