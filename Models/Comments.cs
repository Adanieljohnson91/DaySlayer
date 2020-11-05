using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaySlayer.Models
{
    public class Comments
    {
        /*
         * ID
         * RitualId
         * Comment
         */
        public int Id { get; set; }
        public int RitualId { get; set; }
        public string Comment { get; set; }
    }
}
