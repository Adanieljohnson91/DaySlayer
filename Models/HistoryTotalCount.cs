using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaySlayer.Models
{
    public class HistoryTotalCount
    {
        public List<History> Histories { get; set; }
        public int TotalCount { get; set; }
        public DayStreak DayStreak { get; set; }
    }
}
