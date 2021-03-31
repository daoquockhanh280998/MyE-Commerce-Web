using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CS.EF.Models
{
    [Table("product_main_category", Schema = "IHM")]
    public class ProductMainCategory
    {
        [Column("product_main_category_id")]
        public Guid Id { set; get; }

        [Column("product_main_category_name")]
        public string ProductMainCategoryName { get; set; }

        [Column("sort_order")]
        public int SortOrder { set; get; }

        [Column("status")]
        public bool Status { set; get; }

        [Column("create_by")]
        public string CreateBy { set; get; }
    }
}