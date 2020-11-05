using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaySlayer.Models
{
    public class Pagination
    {
        const int maxPageSize = 50;
        //Should add Total Count.. adding to stretch goal.
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
