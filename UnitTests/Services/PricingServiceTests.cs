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
    }
}
