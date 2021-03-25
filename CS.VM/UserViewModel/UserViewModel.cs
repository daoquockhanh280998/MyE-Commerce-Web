using System;
using System.Collections.Generic;
using System.Text;

namespace CS.VM.UserViewModel
{
    public class UserViewModel
    {        
        public Guid Id { get; set; }
  
        public string UserName { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime? DOB { get; set; }

        public string FullName { get; set; }

        public Guid RoleId { get; set; }

        public bool Status { get; set; }

        public string ImagePath { get; set; }
    }
}
