using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectHouse.Models.Learning
{
    public class HomeWork
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public HomeWorkStatus Status { get; set; }
        public DateTime Date { get; set; }
        public int Position { get; set; }
        public string Text { get; set; }
        public int GroupeId { get; set; }
        public Groupe Groupe { get; set; }
    }
    public enum HomeWorkStatus
    {
        [Display(Name = "In progress")]
        InProgress = 1,
        [Display(Name = "On hold")]
        OnHold,
        Stuck,
        [Display(Name = "Not started")]
        NotStarted,
        Completed
    }
}
