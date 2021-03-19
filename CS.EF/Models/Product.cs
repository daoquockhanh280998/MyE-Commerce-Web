using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CS.EF.Models
{
    [Table("product", Schema = "IHM")]
    public class Product
    {
        [Column("product_id")]
        public Guid ProductID { get; set; }

        [Column("product_name")]
        public string ProductName { get; set; }

        [Column("image_path")]
        public string ImagePath { get; set; }

        [Column("price")]
        public int Price { get; set; }

        [Column("old_price")]
        public decimal OldPrice { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { set; get; } = DateTime.Now;

        [Column("create_by")]
        public string CreateBy { set; get; }
    }
}