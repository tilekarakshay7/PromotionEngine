using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PromotionEngine.Services.Interfaces;

namespace PromotionEngine.Test
{
    [TestClass]
    public class OrderServicesTest
    {
        private Mock<IPromotionRuleServices> _PromotionRuleServicesMock;
        private Mock<IItemServices> _ItemServicesMock;


        [TestInitialize]
        public void Init()
        {
            _PromotionRuleServicesMock = new Mock<IPromotionRuleServices>();
            _ItemServicesMock = new Mock<IItemServices>();

        }

        [TestMethod]
        public void ProcessBill_WhenNoPromotionRule_Then_CalculateTotalPrice()
        {

        }
    }
}
