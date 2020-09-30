using System.Collections.Generic;

namespace PromotionEngine.Models
{
    public class PromotionRule
    {
        public PromotionRule()
        {
            ListOfAnotherItemsToBeConsidered = new List<char>();
        }
        public char SKUId { get; set; }

        public string RuleName { get; set; }

        public int NumberOfApperance { get; set; }

        public int PercentageToReduceFromPrice { get; set; }

        public int LumsumAmountToReduceFromPrice { get; set; }

        /// <summary>
        /// Ex : C + D have combine rule, in case of promotion rule C, it contains "D"
        /// </summary>
        public List<char> ListOfAnotherItemsToBeConsidered { get; set; }
    }
}
