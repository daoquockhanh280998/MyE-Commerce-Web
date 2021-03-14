using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS.VM.Rerponse
{
    public class TableResultJsonResponse<T>
    {
        public int Draw { get; set; }

        [JsonProperty("recordsTotal")]
        public int RecordsTotal { get; set; }

        [JsonProperty("recordsFiltered")]
        public int RecordsFiltered { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public List<T> Data { get; set; }

        /// <summary>
        /// Gets or sets the other.
        /// </summary>
        /// <value>The other.</value>
        public object Other { get; set; }
    }
}