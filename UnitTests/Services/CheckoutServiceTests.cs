using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Models;
using Moq;

namespace UnitTests.Services
{
    [TestClass()]
    public class CheckoutServiceTests
    {
        private CheckoutService Service;

        [TestInitialize()]
        public void Setup()
        {
            Mock<IPricingService> _mockedPricingService = new Mock<IPricingService>();
            Service = new CheckoutService(_mockedPricingService.Object);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Throws_Exception_When_Item_Is_Null()
        {
            Service.Add(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Throws_Exception_When_Count_Less_Than_0()
        {
            Service.Add(new Item("T", 1), -1);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Throws_Exception_When_Count_0()
        {
            Service.Add(new Item("T", 1), 0);
        }
    }
}