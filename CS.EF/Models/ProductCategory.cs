using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AdminApp.EF
{
    [Table("product_category", Schema = "IHM")]
    public class ProductCategory
    {
        [Column("id")]
        public Guid Id { set; get; }
        [Column("product_category_name")]
        public string ProductCategoryName { get; set; }

        [Column("sort_order")]
        public int SortOrder { set; get; }

        [Column("is_show_on_home")]
        public bool IsShowOnHome { set; get; }

        //[Column("parent_id")]
        //public int ParentId { set; get; }

        [Column("status")]
        public int Status { set; get; }

        [Column("create_by")]
        public string CreateBy { set; get; }
    }
}
