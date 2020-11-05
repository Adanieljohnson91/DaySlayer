using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaySlayer.Models
{
    public class DayStreak
    {
        public int Streak { get; set; }

        public static explicit operator DayStreak(Task<DayStreak> v)
        {
            throw new NotImplementedException();
        }
    }
}
