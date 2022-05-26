using System;

namespace ProjectHouse.Models.MyTime
{
    public class TimeTable
    {
        public int Id { get; set; }
        public Project.Project Project { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
    }
}
