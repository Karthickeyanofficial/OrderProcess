using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Promotions
{
    public class SetupOperations:ISetupOperations
    {
        public void AddSKU(string ID,decimal unitPrice)
        {
            SKU.All.Add(new SKU{ID=ID,UnitPrice=unitPrice });
        }

        public void ClearSKUs()
        {
            SKU.All.Clear();
        }

        public void AddDiscount(string name,string expression,decimal price)
        {
            Discount.All.Add(new Discount {Name=name, Expression = expression, Price = price });
        }
        public void ClearDiscounts()
        {
            Discount.All.Clear();
        }

        public Order LoadCart(List<char> items)
        {
            Order order = new Order();            
            order.LineItems.AddRange(items);
            return order;
        }

        public int GetSKUCount()
        {
            return SKU.All.Count;
        }

        public int GetDiscountCount()
        {
            return Discount.All.Count;
        }

        public decimal CalculateCharge(Order order)
        {
            decimal grandTotal = 0.0M;

            List<Discount> discountList=Discount.All;

            Dictionary<string, int> orderItems = new Dictionary<string, int>();
            
            orderItems.Clear();

            foreach (SKU sku in SKU.All)
            {
              if(  order.LineItems.Any(x => x.ToString().Equals(sku.ID)))
                {
                    int count = order.LineItems.Where(x => x.ToString().Equals(sku.ID)).Count();
                    orderItems.Add(sku.ID, count);
                }
            }

            foreach (Discount discount in Discount.All)
            {
                decimal discountPrice;
                orderItems =ApplyDiscount(orderItems, discount, out discountPrice);
                grandTotal = grandTotal + discountPrice;
            }

            foreach (var item in orderItems)
            {
                grandTotal = grandTotal + (SKU.GetSKUPriceBasedOnId(item.Key) * item.Value);

            }

            return grandTotal;
        }

        Dictionary<string,int> ApplyDiscount(Dictionary<string,int> lineItems,Discount discount,out decimal discountPrice)
        {
            int count = 0;
            int div = 0;
            bool isPresent;
            switch(discount.Name)
            {
                case "ThreeA":                    
                    isPresent= lineItems.TryGetValue("A",out count);
                    if (isPresent && count >= 3)
                    {
                        div = (int)(count / 3);
                        discountPrice=div * discount.Price;
                        lineItems["A"] = count - (div * 3);                              
                    }
                    else
                    {
                        discountPrice = 0;
                    }
                    return lineItems;

                case "TwoB":
                    isPresent = lineItems.TryGetValue("B", out count);
                    if (isPresent && count>=2)
                    {
                        div = (int)(count / 2);
                        discountPrice = div * discount.Price;
                        lineItems["B"] = count - (div * 2);
                    }
                    else
                    {
                        discountPrice = 0;
                    }
                    return lineItems;

                case "CnD":
                    isPresent = lineItems.TryGetValue("C", out count);
                    int DCount = 0;
                    bool isDPresent = lineItems.TryGetValue("D", out DCount);

                    if(isPresent && isDPresent && count>=1 && DCount>=1)
                    {
                        int minCount = Math.Min(count, DCount);
                        discountPrice = minCount * discount.Price;
                        lineItems["C"] = count - minCount;
                        lineItems["D"] = count - DCount;

                    }
                    else
                    {
                        discountPrice = 0;
                    }
                    return lineItems;

            }
            discountPrice = 0;
            return lineItems;

        }
    }
}
