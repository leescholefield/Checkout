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

        [TestMethod()]
        public void Returns_Correct_Result_With_Multiples_Of_Same_Item_In_Basket()
        {
            Basket basket = new Basket();
            basket.Add(new Item("T", 5), 2);

            decimal result = Service.CalculateBasketPrice(basket);

            Assert.AreEqual(10, result);
        }

        [TestMethod()]
        public void Returns_Correct_Result_With_Multiple_Copies_Of_Multiple_Items()
        {
            Basket basket = new Basket();
            basket.Add(new Item("T", 5), 2);
            basket.Add(new Item("R", 10), 3);

            decimal result = Service.CalculateBasketPrice(basket);

            Assert.AreEqual(40, result);
        }

        #region PercentagePromotion tests

        [TestMethod()]
        public void PercentagePromotion_Is_Applied_When_Correct_Number_Items_In_Basket()
        {
            _mockedPromoRepo.Setup(x => x.Get("P")).Returns(new PercentagePromotion("P", 2, 10));
            Basket basket = new Basket();
            basket.Add(new Item("P", 10), 2);

            decimal result = Service.CalculateBasketPrice(basket);

            Assert.AreEqual(18m, result);
        }

        [TestMethod()]
        public void PercentagePromotion_Is_Not_Applied_When_Required_Number_Items_Not_Met()
        {
            _mockedPromoRepo.Setup(x => x.Get("P")).Returns(new PercentagePromotion("P", 2, 10));
            Basket basket = new Basket();
            basket.Add(new Item("P", 10), 1);

            decimal result = Service.CalculateBasketPrice(basket);

            Assert.AreEqual(10m, result);
        }

        [TestMethod()]
        public void PercentagePromotion_Is_Not_Applied_To_Any_Additional_Items_If_Count_Not_A_Multiple_Of_Required_Items()
        {
            _mockedPromoRepo.Setup(x => x.Get("P")).Returns(new PercentagePromotion("P", 2, 10));
            Basket basket = new Basket();
            basket.Add(new Item("P", 10), 3);

            decimal result = Service.CalculateBasketPrice(basket);

            Assert.AreEqual(28m, result);
        }

        [TestMethod()]
        public void PercentagePromotion_Is_Stacked()
        {
            _mockedPromoRepo.Setup(x => x.Get("P")).Returns(new PercentagePromotion("P", 2, 10));
            Basket basket = new Basket();
            basket.Add(new Item("P", 10), 4);

            decimal result = Service.CalculateBasketPrice(basket);

            Assert.AreEqual(36m, result);
        }

        [TestMethod()]
        public void PercentagePromotion_Is_Not_Applied_To_Different_Items_In_Basket()
        {
            _mockedPromoRepo.Setup(x => x.Get("P")).Returns(new PercentagePromotion("P", 2, 10));
            Basket basket = new Basket();
            basket.Add(new Item("P", 10), 2);
            basket.Add(new Item("T", 5));

            decimal result = Service.CalculateBasketPrice(basket);

            Assert.AreEqual(23m, result);
        }

        #endregion

        #region SetPricePromotion Tests

        [TestMethod()]
        public void SetPricePromotion_Is_Applied_When_We_Have_Required_Number_Items()
        {
            _mockedPromoRepo.Setup(x => x.Get("S")).Returns(new SetPricePromotion("S", 2, 10));
            Basket basket = new Basket();
            basket.Add(new Item("S", 10), 2);

            decimal result = Service.CalculateBasketPrice(basket);

            Assert.AreEqual(10m, result);
        }

        [TestMethod()]
        public void SetPricePromotion_Is_Not_Applied_When_Required_Items_Not_Met()
        {
            _mockedPromoRepo.Setup(x => x.Get("S")).Returns(new SetPricePromotion("S", 3, 15));
            Basket basket = new Basket();
            basket.Add(new Item("S", 10), 2);

            decimal result = Service.CalculateBasketPrice(basket);

            Assert.AreEqual(20m, result);
        }

        [TestMethod()]
        public void SetPricePromotion_Is_Not_Applied_To_Additional_Items_When_Count_Is_Not_Multiple_Of_RequiredItems()
        {
            _mockedPromoRepo.Setup(x => x.Get("S")).Returns(new SetPricePromotion("S", 2, 15));
            Basket basket = new Basket();
            basket.Add(new Item("S", 10), 3);

            decimal result = Service.CalculateBasketPrice(basket);

            Assert.AreEqual(25m, result);
        }

        [TestMethod()]
        public void SetPricePromotion_Is_Stacked()
        {
            _mockedPromoRepo.Setup(x => x.Get("S")).Returns(new SetPricePromotion("S", 2, 15));
            Basket basket = new Basket();
            basket.Add(new Item("S", 10), 4);

            decimal result = Service.CalculateBasketPrice(basket);

            Assert.AreEqual(30m, result);
        }

        [TestMethod()]
        public void SetPricePromotion_Is_Not_Applied_To_Different_Items()
        {
            _mockedPromoRepo.Setup(x => x.Get("S")).Returns(new SetPricePromotion("S", 2, 15));
            Basket basket = new Basket();
            basket.Add(new Item("S", 10), 2);
            basket.Add(new Item("R", 5), 1);

            decimal result = Service.CalculateBasketPrice(basket);

            Assert.AreEqual(20m, result);
        }

        #endregion

        
    }
}
