using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS.VM.Request
{
    public class UserRequest
    {

        public string UserName { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime? DOB { get; set; }

        public string FullName { get; set; }

        public Guid RoleId { get; set; }

        public bool Status { get; set; }

        public IFormFile ThumbnailImage { get; set; }
    }
}
