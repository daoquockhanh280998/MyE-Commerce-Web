using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CS.EF.Models
{
    [Table("order", Schema = "IHM")]
    public class Order
    {
        [Column("id")]
        public Guid Id { set; get; }

        [Column("order_date")]
        public DateTime OrderDate { set; get; }

        [Column("user_id")]
        public Guid UserId { set; get; }

        [Column("ship_name")]
        public string ShipName { set; get; }

        [Column("ship_address")]
        public string ShipAddress { set; get; }

        [Column("ship_email")]
        public string ShipEmail { set; get; }

        [Column("ship_phone_number")]
        public string ShipPhoneNumber { set; get; }

        [Column("status")]
        public int Status { set; get; }
    }
}