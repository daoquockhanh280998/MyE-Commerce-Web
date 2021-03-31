using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS.VM.Request
{
    public class ProductMainCategoryRequest
    {
        public Guid Id { set; get; }

        public string ProductMainCategoryName { get; set; }

        public int SortOrder { set; get; }

        public bool Status { set; get; }

        public string CreateBy { set; get; }
    }
}