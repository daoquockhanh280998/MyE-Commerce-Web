using System;
using System.Collections.Generic;
using System.Text;

namespace CS.VM.Request
{
    public class ProductRequest
    {
        public Guid ProductID { get; set; }

        public string ProductName { get; set; }

        public int Price { get; set; }

        public int OldPrice { get; set; }

        public DateTime DateCreated { set; get; }

        public string CreateBy { set; get; }
    }
}
