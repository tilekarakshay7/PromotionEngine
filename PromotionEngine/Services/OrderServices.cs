using PromotionEngine.Models;
using PromotionEngine.Services.Interfaces;
using System.Collections.Generic;

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

        public int ProcessBill(List<Cart> carts)
        {
            var total = 0;
            foreach (var cart in carts)
            {
                var promoRule = promotionRuleServices.GetPromotionRulesBySKUId(cart.SKUId);
                total += ApplyPromotionRule(cart, promoRule);
            }
            return total;
        }

        private int ApplyPromotionRule(Cart cart, PromotionRule promoRule)
        {
            var item = itemServices.GetItemBySkuId(cart.SKUId);
            if (promoRule == null)
                return cart.TotalCount * item.Price;

            return 0;
        }

    }
}
