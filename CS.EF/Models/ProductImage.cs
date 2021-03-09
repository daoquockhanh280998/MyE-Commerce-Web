using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AdminApp.EF
{
    [Table("product_image", Schema = "IHM")]
    public class ProductImage
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("product_id")]
        public Guid ProductId { get; set; }

        [Column("image_path")]
        public string ImagePath { get; set; }

        [Column("caption")]
        public string Caption { get; set; }

        [Column("is_default")]
        public bool IsDefault { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { get; set; }

        [Column("sort_order")]
        public int SortOrder { get; set; }

        [Column("file_size")]
        public long FileSize { get; set; }

    }
}
