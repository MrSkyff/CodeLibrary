using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectHouse.Models.Learning
{
    public class Groupe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public DateTime Date { get; set; }
        public GroupeStatus Status { get; set; }
    }
    public enum GroupeStatus
    {
        [Display(Name = "Groupe not Completed")]
        NotCompleted = 1,
        [Display(Name = "Groupe completed")]
        Completed
    }
}
