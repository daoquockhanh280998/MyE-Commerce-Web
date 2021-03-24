using System;
using System.Collections.Generic;
using System.Text;

namespace CS.VM.ProductViewModel
{
    public class ProductViewModel
    {
        public Guid ProductID { get; set; }

        public string ProductName { get; set; }
        public string ImagePath { get; set; }

        public int Price { get; set; }

        public decimal OldPrice { get; set; }

        public DateTime? DateCreated { set; get; }

        public string CreateBy { set; get; }
        public bool Status { set; get; }
    }
}