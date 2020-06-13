using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    public class Discount
    {
        public string Name { get; set; }
        public string Expression { get; set; }

        public decimal Price { get; set; }

        public static List<Discount> All { get; set; } = new List<Discount>();
    }
}
