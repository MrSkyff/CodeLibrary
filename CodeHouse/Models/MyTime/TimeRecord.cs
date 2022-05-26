using System;
using System.Collections.Generic;

namespace ProjectHouse.Models.MyTime
{
    public class TimeRecord
    {
        public int Id { get; set; }
        public string Notes { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; } = null;
        public int ProjectId { get; set; }
    }
}
