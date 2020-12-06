using Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests.Models
{
    [TestClass()]
    public class BasketTests
    {

        private Basket basket;

        [TestInitialize()]
        public void Setup()
        {
            basket = new Basket();
        }

        [TestMethod()]
        public void Add_Item()
        {
            basket.Add(new Item("A", 10));

            Assert.AreEqual(1, basket["A"]);
        }

        [TestMethod()]
        public void Add_Multiple_Of_Same_Item()
        {
            basket.Add(new Item("A", 10), 3);

            Assert.AreEqual(3, basket["A"]);
        }

        [TestMethod()]
        public void Count_Returns_Correct_Amount()
        {
            basket.Add(new Item("A", 10), 2);
            basket.Add(new Item("B", 5), 3);

            Assert.AreEqual(5, basket.Count);
        }

        [TestMethod()]
        public void Adding_Null_Item_Throws_Exception()
        {
            Assert.ThrowsException<ArgumentNullException>(() => basket.Add(null));
        }

        [TestMethod()]
        public void Adding_Minus_Amount_Throws_Exception()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => basket.Add(new Item("A", 10), -1));
        }
    }
}
