using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Order
    {
        public List<char> LineItems { get; set; } = new List<char>();
    }
}
