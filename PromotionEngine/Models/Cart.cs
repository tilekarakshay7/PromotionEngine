namespace PromotionEngine.Models
{
    public class Cart
    {
        public Cart(char skuId, int total)
        {
            SKUId = skuId;
            CountOfRemainingItemsForPromo = TotalCount = total;
        }
        public char SKUId { get; set; }
        public int TotalCount { get; set; }

        /// <summary>
        /// count of item process after promotion
        /// TotalCount is 5 and lets assume 3 items are processed then 2 will assign to "CountOfRemainingItemsForPromo"
        /// </summary>
        public int CountOfRemainingItemsForPromo { get; set; }
    }
}
