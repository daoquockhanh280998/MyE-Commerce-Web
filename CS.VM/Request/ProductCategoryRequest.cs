using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS.VM.Request
{
    public class ProductCategoryRequest
    {
        public Guid Id { set; get; }

        public string ProductCategoryName { get; set; }

        public int SortOrder { set; get; }

        public bool IsShowOnHome { set; get; }

        public Guid ProductMainCategoryId { set; get; }

        public bool Status { set; get; }

        public string CreateBy { set; get; }
    }
}