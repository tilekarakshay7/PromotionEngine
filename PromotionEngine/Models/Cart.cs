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
        public int CountOfRemainingItemsForPromo { get; set; }
    }
}
