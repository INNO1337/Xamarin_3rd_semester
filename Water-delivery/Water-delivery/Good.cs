using System;
using System.Collections.Generic;
using System.Text;

namespace Water_delivery
{
    public class Good
    {
        public string name { get; set; }
        public UInt64 quantity { get; set; }
        public UInt64 price { get; set; }

        public Dictionary<string, UInt64> goods = new Dictionary<string, UInt64>();

    }

}