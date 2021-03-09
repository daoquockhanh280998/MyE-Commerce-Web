﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AdminApp.EF
{
    [Table("product", Schema = "IHM")]
    public class Product
    {
        [Column("product_id")]
        public Guid ProductID { get; set; }

        [Column("product_name")]
        public string ProductName { get; set; }

        [Column("price")]
        public int Price { get; set; }

        [Column("old_price")]
        public decimal OldPrice { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { set; get; }

        [Column("create_by")]
        public string CreateBy { set; get; }

    }
}
