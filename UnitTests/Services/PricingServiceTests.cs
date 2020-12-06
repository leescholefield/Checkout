using Core.Models;
using Core.Repository;
using Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests.Services
{
    [TestClass()]
    public class PricingServiceTests
    {
        private PricingService Service;
        // mocks
        private Mock<IRepository<Promotion>> _mockedPromoRepo;

        [TestInitialize()]
        public void Setup()
        {
            _mockedPromoRepo = new Mock<IRepository<Promotion>>();
            Service = new PricingService(_mockedPromoRepo.Object);
        }

        [TestMethod()]
        public void Returns_0_When_Basket_Is_Empty()
        {
            Basket basket = new Basket();
            decimal result = Service.CalculateBasketPrice(basket);

            Assert.AreEqual(0m, result);
        }

        [TestMethod()]
        public void Returns_Correct_Result_With_Single_Item_In_Basket()
        {
            Basket basket = new Basket();
            basket.Add(new Item("T", 5));

            decimal result = Service.CalculateBasketPrice(basket);

            Assert.AreEqual(5m, result);
        }

        [TestMethod()]
        public void Returns_Correct_Result_With_Different_Items_In_Basket()
        {
            Basket basket = new Basket();
            basket.Add(new Item("T", 5));
            basket.Add(new Item("R", 10));

            decimal result = Service.CalculateBasketPrice(basket);

            Assert.AreEqual(15m, result);
        }
    }
}
