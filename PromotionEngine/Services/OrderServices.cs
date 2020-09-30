using PromotionEngine.Models;
using PromotionEngine.Services.Interfaces;
using System;
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
                if (cart.CountOfRemainingItemsForPromo != 0)
                {
                    var promoRule = promotionRuleServices.GetPromotionRulesBySKUId(cart.SKUId);
                    total += ApplyPromotionRule(carts, cart, promoRule);
                }
            }
            return total;
        }

        private int ApplyPromotionRule(List<Cart> carts, Cart cart, PromotionRule promoRule)
        {

            var item = itemServices.GetItemBySkuId(cart.SKUId);
            if (promoRule == null)
                return cart.CountOfRemainingItemsForPromo * item.Price; // "CountOfRemainingItemsForPromo" test case Scenario6

            if (promoRule.LumsumAmountToReduceFromPrice > 0)
            {
                return CalculatePrice(carts, cart, promoRule);
            }
            if (promoRule.PercentageToReduceFromPrice > 0)
            {
                // we can add future requirement here Ex : discount based on percentage
            }
            throw new Exception("invalid input...");
        }

        private int CalculatePrice(List<Cart> carts, Cart cart, PromotionRule promoRule)
        {
            var price = 0;
            var item = itemServices.GetItemBySkuId(cart.SKUId);

            // process untill item count are applicable for promo rules
            while (promoRule.NumberOfApperance <= cart.CountOfRemainingItemsForPromo && cart.CountOfRemainingItemsForPromo != 0)
            {
                var IsOtherItemExistInCart = true;
                foreach (var otherItem in promoRule.ListOfAnotherItemsToBeConsidered)
                {
                    var anotherItem = itemServices.GetItemBySkuId(otherItem);
                    IsOtherItemExistInCart = carts.Any(x => x.SKUId == anotherItem.SKUId); // for rule like : C + D = x amount
                    if (IsOtherItemExistInCart) // if other item is in cart
                    {
                        price += anotherItem.Price; // add other item price in total price
                        var cartItem = carts.First(x => x.SKUId == anotherItem.SKUId);
                        cartItem.CountOfRemainingItemsForPromo = cartItem.CountOfRemainingItemsForPromo - promoRule.NumberOfApperance;
                    }
                }
                if (IsOtherItemExistInCart) // if yes : Apply promo rule
                    price += item.Price * promoRule.NumberOfApperance - promoRule.LumsumAmountToReduceFromPrice;
                else // not exist then normal calculation
                    price += item.Price * cart.CountOfRemainingItemsForPromo;

                // remove no of items processed (so will get count of items that can process with promo rule)
                cart.CountOfRemainingItemsForPromo = cart.CountOfRemainingItemsForPromo - promoRule.NumberOfApperance;
            }

            if (cart.CountOfRemainingItemsForPromo > 0) // remaining item after promo rule, will process with normal calulation
            {
                price += (item.Price * cart.CountOfRemainingItemsForPromo);
                cart.CountOfRemainingItemsForPromo = 0; // all item are processed
            }
            return price;
        }

    }
}
