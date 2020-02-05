using System;
using System.Collections.Generic;
using System.Text;

namespace Water_delivery
{
    class JsonItem
    {
        public JsonItem()
        {

        }
        
        public JsonItem(string Name_, UInt64 Count_, UInt64 Price_)
        {
            Name = Name_;
            Count = Count_;
            Price = Price_;
        }

        public string Name { get; set; }
        public UInt64 Count { get; set; }
        public UInt64 Price { get; set; }
    }
}
