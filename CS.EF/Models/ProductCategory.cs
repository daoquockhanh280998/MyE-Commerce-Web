using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CS.EF.Models
{
    [Table("product_category", Schema = "IHM")]
    public class ProductCategory
    {
        [Column("product_category_id")]
        public Guid Id { set; get; }

        [Column("product_category_name")]
        public string ProductCategoryName { get; set; }

        [Column("sort_order")]
        public int SortOrder { set; get; }

        [Column("is_show_on_home")]
        public bool IsShowOnHome { set; get; }

        [Column("product_main_category_id")]
        public Guid ProductMainCategoryId { set; get; }

        [Column("status")]
        public bool Status { set; get; }

        [Column("create_by")]
        public string CreateBy { set; get; }
    }
}