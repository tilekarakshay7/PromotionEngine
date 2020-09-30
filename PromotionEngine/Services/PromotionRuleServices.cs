using PromotionEngine.Helpers;
using PromotionEngine.Models;
using PromotionEngine.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Services
{
    public class PromotionRuleServices : IPromotionRuleServices
    {
        // in actual case, we fetch from DB
        public List<PromotionRule> GetPromotionRules()
        {
            List<PromotionRule> PromotionRules = new List<PromotionRule>()
            {
                new PromotionRule
                {
                    RuleName = "3_A_For_130 ",
                    SKUId = Constants.A,
                    NumberOfApperance = 3 ,
                    LumsumAmountToReduceFromPrice = 20,
                    PercentageToReduceFromPrice = 0
                },
                new PromotionRule
                {
                    RuleName = "2_B_For_45",
                    SKUId = Constants.B,
                    NumberOfApperance = 2 ,
                    LumsumAmountToReduceFromPrice = 15,
                    PercentageToReduceFromPrice = 0
                },
                new PromotionRule
                {
                    RuleName = "C_&_D_For_30",
                    SKUId = Constants.C,
                    NumberOfApperance = 1,
                    LumsumAmountToReduceFromPrice = 5,
                    PercentageToReduceFromPrice = 0,
                    ListOfAnotherItemsToBeConsidered = new List<char>() { Constants.D }
                },
            };
            return PromotionRules;
        }

        public PromotionRule GetPromotionRulesBySKUId(char SKUId)
        {
            return GetPromotionRules().FirstOrDefault(x => x.SKUId == SKUId);
        }
    }
}
