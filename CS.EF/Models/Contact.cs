using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AdminApp.EF
{
    [Table("contact", Schema = "IHM")]
    public class Contact
    {
        [Column("id")]
        public Guid Id { set; get; }

        [Column("full_name")]
        public string FullName { set; get; }

        [Column("email")]
        public string Email { set; get; }

        [Column("phone_number")]
        public string PhoneNumber { set; get; }

        [Column("message")]
        public string Message { set; get; }

        [Column("status")]
        public int Status { set; get; }

    }
}
