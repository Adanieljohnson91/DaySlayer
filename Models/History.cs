using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaySlayer.Models
{
    public class History
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Hrs_Of_Sleep { get; set; }
        public bool Physical { get; set; }
        public bool Spiritual { get; set; }
        public bool Mental { get; set; }
        public int RitualId { get; set; }
        public Ritual Ritual { get; set; }
        public int Result { get; set; }
        public int Time_To_Bed { get; set; }
        public string Note { get; set; }
        public int UserId { get; set; }
        public DayStreak DayStreak { get; set; }
    }
}
