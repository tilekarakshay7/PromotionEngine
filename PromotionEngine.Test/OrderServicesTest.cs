using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PromotionEngine.Helpers;
using PromotionEngine.Models;
using PromotionEngine.Services;
using PromotionEngine.Services.Interfaces;
using System.Collections.Generic;

namespace PromotionEngine.Test
{
    [TestClass]
    public class OrderServicesTest
    {
        private Mock<IPromotionRuleServices> _PromotionRuleServicesMock;
        private Mock<IItemServices> _ItemServicesMock;
        private OrderServices OrderServices;

        [TestInitialize]
        public void Init()
        {
            _PromotionRuleServicesMock = new Mock<IPromotionRuleServices>();
            _ItemServicesMock = new Mock<IItemServices>();
            OrderServices = new OrderServices(_PromotionRuleServicesMock.Object, _ItemServicesMock.Object);
        }

        [TestMethod]
        public void ProcessBill_WhenNoPromotionRule_Then_CalculateTotalPrice()
        {
            //Arrange
            var carts = new List<Cart>()
            {
                new Cart(Constants.A,  2)
            };

            var promotionRule = new PromotionRule
            {
                RuleName = "Rule_A",
                SKUId = Constants.A,
                NumberOfApperance = 3,
                LumsumAmountToReduceFromPrice = 20,
                PercentageToReduceFromPrice = 0
            };

            var item = new Item() { SKUId = Constants.A, Name = "A Name", Price = 50 };

            _PromotionRuleServicesMock.Setup(x => x.GetPromotionRulesBySKUId(Constants.A)).Returns<PromotionRule>(null);
            _ItemServicesMock.Setup(x => x.GetItemBySkuId(Constants.A)).Returns(item);

            //Act
            var totalPrice = OrderServices.ProcessBill(carts);

            //Assert
            Assert.AreEqual(totalPrice, 100);

        }

        [TestMethod]
        public void ProcessBill_WhenPromotionRuleIsApplied_Then_CalculateTotalPrice_Scenario1()
        {
            // 3 A : final total cost : 150 - 30 = 120

            //Arrange
            var carts = new List<Cart>()
            {
                new Cart(Constants.A,  3)
            };

            var promotionRule = new PromotionRule
            {
                RuleName = "Rule_A",
                SKUId = Constants.A,
                NumberOfApperance = 3,
                LumsumAmountToReduceFromPrice = 20,
                PercentageToReduceFromPrice = 0
            };

            var item = new Item() { SKUId = Constants.A, Name = "A Name", Price = 50 };

            _PromotionRuleServicesMock.Setup(x => x.GetPromotionRulesBySKUId(Constants.A)).Returns(promotionRule);
            _ItemServicesMock.Setup(x => x.GetItemBySkuId(Constants.A)).Returns(item);

            //Act
            var totalPrice = OrderServices.ProcessBill(carts);

            //Assert
            Assert.AreEqual(totalPrice, 130);

        }

        [TestMethod]
        public void ProcessBill_WhenPromotionRuleIsApplied_Then_CalculateTotalPrice_Scenario2()
        {
            // 4 A : final total cost : (150 - 30) + 50 = 180

            //Arrange
            var carts = new List<Cart>()
            {
                new Cart(Constants.A,  4)
            };

            var promotionRule = new PromotionRule
            {
                RuleName = "Rule_A",
                SKUId = Constants.A,
                NumberOfApperance = 3,
                LumsumAmountToReduceFromPrice = 20,
                PercentageToReduceFromPrice = 0
            };

            var item = new Item() { SKUId = Constants.A, Name = "A Name", Price = 50 };

            _PromotionRuleServicesMock.Setup(x => x.GetPromotionRulesBySKUId(Constants.A)).Returns(promotionRule);
            _ItemServicesMock.Setup(x => x.GetItemBySkuId(Constants.A)).Returns(item);

            //Act
            var totalPrice = OrderServices.ProcessBill(carts);

            //Assert
            Assert.AreEqual(totalPrice, 180);

        }
    }
}
