using System;
using System.Collections.Generic;
using System.Text;

namespace CS.VM.Request
{
    public class PaggingRequest
    {
        public string keyword { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}