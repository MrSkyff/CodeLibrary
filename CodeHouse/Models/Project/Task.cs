using System;

namespace ProjectHouse.Models.Project
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public string Text { get; set; }
        public int Position { get; set; }
        public int CategoryId { get; set; }
        public DateTime Date { get; set; }
        public DateTime DeadLine { get; set; }
        public bool Global { get; set; }
        public TaskCategory Category { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public TypeOfTask TypeOfTask { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public TaskImportance TaskImportance { get; set; }
    }
    public enum TaskStatus
    {
        Active = 1,
        InActive,
        Freezed,
        Completed
    }
    public enum TaskImportance
    {
        Critical = 1,
        Hight,
        Normal,
        Low
    }
    public enum TypeOfTask
    {
        NoType,
        BugFix,
        NewContent,
        Extension,
        Refactoring
    }

}
