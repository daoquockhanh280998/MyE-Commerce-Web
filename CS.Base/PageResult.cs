using System;
using System.Collections.Generic;
using System.Text;

namespace CS.Base
{
    public class PageResult<T>
    {
        public List<T> Items { get; set; }

        public int TotalRecord { get; set; }
    }
}