using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS.VM.Request
{
    public class BaseRequest
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonRequired]
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
