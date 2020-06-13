using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Promotions
{
    interface ISetupOperations
    {
        void AddSKU(string ID,decimal unitPrice);

        int GetSKUCount();

        void AddDiscount(string name, string expression, decimal price);

        int GetDiscountCount();

        Order LoadCart(List<char> items);

        decimal CalculateCharge(Order order);

        void ClearSKUs();

        void ClearDiscounts();
    }
}
