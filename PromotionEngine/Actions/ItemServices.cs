using PromotionEngine.Helpers;
using PromotionEngine.Models;
using System.Collections.Generic;

namespace PromotionEngine.Actions
{
    public class ItemServices
    {
        //in real case, we fetch this from DB
        public List<Item> GetItems()
        {
            return new List<Item>()
            {
                new Item() { SKUId = Constants.A, Name = "A Name", Price = 50 },
                new Item() { SKUId = Constants.B, Name = "B Name", Price = 30 },
                new Item() { SKUId = Constants.C, Name = "C Name", Price = 20 },
                new Item() { SKUId = Constants.D, Name = "D Name", Price = 15 }
            };
        }
    }
}
