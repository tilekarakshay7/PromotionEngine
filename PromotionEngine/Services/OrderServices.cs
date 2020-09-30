using PromotionEngine.Services.Interfaces;

namespace PromotionEngine.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IPromotionRuleServices promotionRuleServices;
        private readonly IItemServices itemServices;

        public OrderServices(IPromotionRuleServices promotionRuleServices, IItemServices itemServices)
        {
            this.promotionRuleServices = promotionRuleServices;
            this.itemServices = itemServices;
        }


    }
}
