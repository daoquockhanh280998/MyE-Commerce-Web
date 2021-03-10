using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CS.EF.Models
{
    [Table("roles", Schema = "IHM")]
    public class Roles
    {
        [Column("role_id")]
        public Guid RoleId { get; set; }

        [Column("role_name")]
        public string RoleName { get; set; }
    }
}