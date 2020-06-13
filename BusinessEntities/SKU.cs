using System;
using System.Collections.Generic;
using System.Linq;
namespace BusinessEntities
{
    public class SKU
    {
        public  string ID { get; set; }

        public  decimal UnitPrice { get; set; }

        public static List<SKU> All { get; set; } = new List<SKU>();

        public static decimal GetSKUPriceBasedOnId(string ID)
        {
            var sku = SKU.All.Where(sku => sku.ID.ToUpper().Equals(ID.ToUpper())).SingleOrDefault();

            if (sku != null)
                return sku.UnitPrice;
            else
                return 0;
        }
    }

}
