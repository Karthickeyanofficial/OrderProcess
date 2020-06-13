using BusinessEntities;
using NUnit.Framework;
using System.Collections.Generic;

namespace Promotions.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            AddSKUs();
            AddDiscounts();
        }

        void AddSKUs()
        {
            SetupOperations setup = new SetupOperations();
            setup.ClearSKUs();
            setup.AddSKU("A", 50);
            setup.AddSKU("B", 30);
            setup.AddSKU("C", 20);
            setup.AddSKU("D", 15);
        }

        public void AddDiscounts()
        {
            SetupOperations setup = new SetupOperations();
            setup.ClearDiscounts();
            setup.AddDiscount("ThreeA","3A", 130);
            setup.AddDiscount("TwoB","2B", 45);
            setup.AddDiscount("CnD","C+D", 30);
        }


        [Test]
        public void Add_SKUs_Success()
        {
            Assert.AreEqual(new SetupOperations().GetSKUCount(), 4);
        }

        [Test]
        public void Add_Discounts_Success()
        {
            Assert.AreEqual(new SetupOperations().GetDiscountCount(), 3);
        }

        [Test]
        public void Test_Scenario_One()
        {
            SetupOperations setup = new SetupOperations();
            Order order1=setup.LoadCart(new List<char> { 'A', 'B', 'C' });
            decimal grandTotal=setup.CalculateCharge(order1);
            Assert.AreEqual((decimal)100,grandTotal);
        }

        [Test]
        public void Test_Scenario_Two()
        {
            SetupOperations setup = new SetupOperations();
            Order order2 = setup.LoadCart(new List<char> { 'A','A','A','A','A','B','B','B','B', 'B', 'C' });
            decimal grandTotal=setup.CalculateCharge(order2);
            Assert.AreEqual( (decimal)370,grandTotal);
        }

        [Test]
        public void Test_Scenario_Three()
        {
            SetupOperations setup = new SetupOperations();
            Order order3 = setup.LoadCart(new List<char> { 'A', 'A', 'A',  'B', 'B', 'B', 'B', 'B', 'C','D' });
            decimal grandTotal = setup.CalculateCharge(order3);
            Assert.AreEqual((decimal)280, grandTotal);
        }
    }
}