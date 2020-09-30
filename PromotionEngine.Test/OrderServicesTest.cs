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

        [TestMethod]
        public void ProcessBill_WhenPromotionRuleIsApplied_Then_CalculateTotalPrice_Scenario3()
        {
            // 2 B : final total cost : (30 + 30) - 15 = 45

            //Arrange
            var carts = new List<Cart>()
            {
                new Cart(Constants.B,2)
            };

            var promotionRule = new PromotionRule
            {
                RuleName = "Rule_B",
                SKUId = Constants.B,
                NumberOfApperance = 2,
                LumsumAmountToReduceFromPrice = 15,
                PercentageToReduceFromPrice = 0
            };

            var item = new Item() { SKUId = Constants.B, Name = "B Name", Price = 30 };

            _PromotionRuleServicesMock.Setup(x => x.GetPromotionRulesBySKUId(Constants.B)).Returns(promotionRule);
            _ItemServicesMock.Setup(x => x.GetItemBySkuId(Constants.B)).Returns(item);

            //Act
            var totalPrice = OrderServices.ProcessBill(carts);

            //Assert
            Assert.AreEqual(totalPrice, 45);

        }

        [TestMethod]
        public void ProcessBill_WhenPromotionRuleIsApplied_Then_CalculateTotalPrice_Scenario4()
        {
            // 3 B : final total cost : (30 + 30 + 30) - 15 = 75

            //Arrange
            var carts = new List<Cart>()
            {
                new Cart(Constants.B,3)
            };

            var promotionRule = new PromotionRule
            {
                RuleName = "Rule_B",
                SKUId = Constants.B,
                NumberOfApperance = 2,
                LumsumAmountToReduceFromPrice = 15,
                PercentageToReduceFromPrice = 0
            };

            var item = new Item() { SKUId = Constants.B, Name = "B Name", Price = 30 };

            _PromotionRuleServicesMock.Setup(x => x.GetPromotionRulesBySKUId(Constants.B)).Returns(promotionRule);
            _ItemServicesMock.Setup(x => x.GetItemBySkuId(Constants.B)).Returns(item);

            //Act
            var totalPrice = OrderServices.ProcessBill(carts);

            //Assert
            Assert.AreEqual(totalPrice, 75);

        }

        [TestMethod]
        public void ProcessBill_WhenPromotionRuleIsApplied_Then_CalculateTotalPrice_Scenario5()
        {
            // C + D : final total cost : (20 + 15)- 5 = 30

            //Arrange
            var carts = new List<Cart>()
            {
                new Cart(Constants.C,1),
                new Cart(Constants.D,1)
            };

            var promotionRule = new PromotionRule
            {
                RuleName = "Rule_C",
                SKUId = Constants.C,
                NumberOfApperance = 1,
                LumsumAmountToReduceFromPrice = 5,
                PercentageToReduceFromPrice = 0,
                ListOfAnotherItemsToBeConsidered = new List<char>() { Constants.D }
            };

            var itemC = new Item() { SKUId = Constants.C, Name = "C Name", Price = 20 };
            var itemD = new Item() { SKUId = Constants.D, Name = "D Name", Price = 15 };

            _PromotionRuleServicesMock.Setup(x => x.GetPromotionRulesBySKUId(Constants.C)).Returns(promotionRule);
            _ItemServicesMock.Setup(x => x.GetItemBySkuId(Constants.C)).Returns(itemC);
            _ItemServicesMock.Setup(x => x.GetItemBySkuId(Constants.D)).Returns(itemD);


            //Act
            var totalPrice = OrderServices.ProcessBill(carts);

            //Assert
            Assert.AreEqual(totalPrice, 30);

        }

        [TestMethod]
        public void ProcessBill_WhenPromotionRuleIsApplied_Then_CalculateTotalPrice_Scenario6()
        {
            // C + 2D : final total cost :  1 CD = 30 and 1D = 15 -> 45

            //Arrange
            var carts = new List<Cart>()
            {
                new Cart(Constants.C,1),
                new Cart(Constants.D,2)
            };

            var promotionRule = new PromotionRule
            {
                RuleName = "Rule_C",
                SKUId = Constants.C,
                NumberOfApperance = 1,
                LumsumAmountToReduceFromPrice = 5,
                PercentageToReduceFromPrice = 0,
                ListOfAnotherItemsToBeConsidered = new List<char>() { Constants.D }
            };

            var itemC = new Item() { SKUId = Constants.C, Name = "C Name", Price = 20 };
            var itemD = new Item() { SKUId = Constants.D, Name = "D Name", Price = 15 };

            _PromotionRuleServicesMock.Setup(x => x.GetPromotionRulesBySKUId(Constants.C)).Returns(promotionRule);
            _ItemServicesMock.Setup(x => x.GetItemBySkuId(Constants.C)).Returns(itemC);
            _ItemServicesMock.Setup(x => x.GetItemBySkuId(Constants.D)).Returns(itemD);

            //Act
            var totalPrice = OrderServices.ProcessBill(carts);

            //Assert
            Assert.AreEqual(totalPrice, 45);

        }

    }
}
