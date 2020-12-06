using Core.Models;
using Core.Repository;
using Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionalTests
{
    /// <summary>
    /// These are functional tests merely to show our implementation is correct. 
    /// 
    /// Since the spec sheet specifies that the item prices change frequently I have used a test version of <see cref="IRepository{T}"/>
    /// so the item prices are not baked into the tests -- which is obviously a bad idea. I haven't done the same thing for the 
    /// <see cref="PromotionRepository"/> but it would be simple enough to implement.
    /// </summary>
    [TestClass()]
    public class CheckoutServiceTests
    {

        private ICheckoutService CheckoutService;
        private static IPricingService PricingService;
        private static IRepository<Item> ItemRepository;

        #region Setup

        [ClassInitialize()]
        public static void Setup(TestContext _)
        {
            PricingService = new PricingService(new PromotionRepository());
            ItemRepository = new TestItemRepository();
        }

        [TestInitialize()]
        public void CheckoutSetup()
        {
            CheckoutService = new CheckoutService(PricingService);
        }

        #endregion

        [TestMethod()]
        public void Item_D_Has_Correct_Discounts_Applied()
        {
            CheckoutService.Add(ItemRepository.Get("D"), 2);

            decimal price = CheckoutService.GetTotal();

            Assert.AreEqual(82.50m, price);
        }

        [TestMethod()]
        public void Item_B_Has_Correct_Discounts_Applied()
        {
            CheckoutService.Add(ItemRepository.Get("B"), 3);

            decimal price = CheckoutService.GetTotal();

            Assert.AreEqual(40, price);
        }

        [TestMethod()]
        public void Both_Item_B_And_Item_D_Promotions_Are_Applied()
        {
            CheckoutService.Add(ItemRepository.Get("B"), 3);
            CheckoutService.Add(ItemRepository.Get("D"), 2);

            decimal price = CheckoutService.GetTotal();

            Assert.AreEqual(122.50m, price);
        }

        [TestMethod()]
        public void Item_B_Promotion_Is_Not_Applied_To_Additional_Items()
        {
            CheckoutService.Add(ItemRepository.Get("B"), 4);

            decimal price = CheckoutService.GetTotal();

            Assert.AreEqual(55, price);
        }

        [TestMethod()]
        public void Item_D_Promotion_Is_Not_Applied_To_Additional_Items()
        {
            CheckoutService.Add(ItemRepository.Get("D"), 3);

            decimal price = CheckoutService.GetTotal();

            Assert.AreEqual(137.50m, price);
        }

        [TestMethod()]
        public void Adding_Many_Items_Returns_Correct_Total()
        {
            CheckoutService.Add(ItemRepository.Get("A"), 1); // 10
            CheckoutService.Add(ItemRepository.Get("B"), 2); // 30
            CheckoutService.Add(ItemRepository.Get("C"), 1); // 40
            CheckoutService.Add(ItemRepository.Get("D"), 1); // 55

            decimal total = CheckoutService.GetTotal();

            Assert.AreEqual(135m, total);
        }
    }

    class TestItemRepository : IRepository<Item>
    {
        public Item Get(string id)
        {
            return id switch
            {
                "A" => new Item("A", 10),
                "B" => new Item("B", 15),
                "C" => new Item("C", 40),
                "D" => new Item("D", 55),
                _ => throw new ArgumentException(id + " is not recognised"),
            };
        }
    }
}
