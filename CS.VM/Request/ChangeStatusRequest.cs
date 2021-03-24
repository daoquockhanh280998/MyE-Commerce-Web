using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS.VM.Request
{
   public class ChangeStatusRequest 
    {
        [JsonRequired]
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
