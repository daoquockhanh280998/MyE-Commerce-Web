using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS.VM.Request
{
    public class LoginRequest
    {
        [JsonProperty("user_name")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}