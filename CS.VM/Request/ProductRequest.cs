using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS.VM.Request
{
    public class ProductRequest
    {
        public string ProductName { get; set; }

        public int Price { get; set; }

        public int OldPrice { get; set; }

        public DateTime? CreatedDate { set; get; }

        public string CreateBy { get; set; }

        public IFormFile ThumbnailImage { get; set; }
       // public bool Status { get; set; }
        public DateTime? UpdateDate { set; get; }
        public string UpdateBy { set; get; }

    }
}