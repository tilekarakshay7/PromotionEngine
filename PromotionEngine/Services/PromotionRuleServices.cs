using PromotionEngine.Helpers;
using PromotionEngine.Models;
using PromotionEngine.Services.Interfaces;
using System.Collections.Generic;

namespace PromotionEngine.Services
{
    public class PromotionRuleServices : IPromotionRuleServices
    {
        public List<PromotionRule> GetPromotionRules()
        {
            List<PromotionRule> PromotionRules = new List<PromotionRule>()
            {
                new PromotionRule
                {
                    RuleName = "Rule_A",
                    SKUId = Constants.A,
                    NumberOfApperance = 3 ,
                    LumsumAmountToReduceFromPrice = 20,
                    PercentageToReduceFromPrice = 0
                },
                new PromotionRule
                {
                    RuleName = "Rule_B",
                    SKUId = Constants.B,
                    NumberOfApperance = 2 ,
                    LumsumAmountToReduceFromPrice = 15,
                    PercentageToReduceFromPrice = 0
                },
            };
            return PromotionRules;
        }
    }
}
