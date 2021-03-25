using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CS.EF.Models
{
    [Table("users", Schema = "IHM")]
    public class Users
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_name")]
        public string UserName { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("dob")]
        public DateTime DOB { get; set; }

        [Column("full_name")]
        public string FullName { get; set; }

        [Column("role_id")]
        public Guid RoleId { get; set; }
        [Column("status")]
        public bool Status { get; set; }

        [Column("image_path")]
        public string ImagePath { get; set; }
    }
}