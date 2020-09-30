using PromotionEngine.Models;
using PromotionEngine.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
                total += ApplyPromotionRule(carts, cart, promoRule);
            }
            return total;
        }

        private int ApplyPromotionRule(List<Cart> carts, Cart cart, PromotionRule promoRule)
        {
            var item = itemServices.GetItemBySkuId(cart.SKUId);
            if (promoRule == null)
                return cart.TotalCount * item.Price;

            if (promoRule.LumsumAmountToReduceFromPrice > 0)
            {
                return CalculatePrice(carts, cart, promoRule);
            }

            return 0;
        }

        private int CalculatePrice(List<Cart> carts, Cart cart, PromotionRule promoRule)
        {
            var price = 0;
            var item = itemServices.GetItemBySkuId(cart.SKUId);

            while (promoRule.NumberOfApperance <= cart.CountOfRemainingItemsForPromo && cart.CountOfRemainingItemsForPromo != 0)
            {
                foreach (var otherItem in promoRule.ListOfAnotherItemsToBeConsidered)
                {
                    var anotherItem = itemServices.GetItemBySkuId(otherItem);
                    var IsOtherItemExistInCart = carts.Any(x => x.SKUId == anotherItem.SKUId);
                    if (IsOtherItemExistInCart)
                    {
                        price += anotherItem.Price;
                        var cartItem = carts.First(x => x.SKUId == anotherItem.SKUId);
                        cartItem.CountOfRemainingItemsForPromo = cartItem.CountOfRemainingItemsForPromo - promoRule.NumberOfApperance;
                    }
                }
                price += item.Price * promoRule.NumberOfApperance - promoRule.LumsumAmountToReduceFromPrice;
                cart.CountOfRemainingItemsForPromo = cart.CountOfRemainingItemsForPromo - promoRule.NumberOfApperance;
            }

            return price;
        }

    }
}
