using System.Collections.Generic;

namespace PromotionEngine.Models
{
    public class PromotionRule
    {
        public char SKUId { get; set; }

        public string RuleName { get; set; }

        public int NumberOfApperance { get; set; }

        public int PercentageToReduceFromPrice { get; set; }

        public int LumsumAmountToReduceFromPrice { get; set; }

        public List<string> ListOfAnotherItemsToBeConsidered { get; set; }
    }
}
