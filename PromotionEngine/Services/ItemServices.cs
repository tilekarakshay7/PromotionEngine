﻿using PromotionEngine.Helpers;
using PromotionEngine.Models;
using PromotionEngine.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Services
{
    public class ItemServices : IItemServices
    {
        public Item GetItemBySkuId(char sKUId)
        {
            return GetItems().FirstOrDefault(x => x.SKUId == sKUId);
        }

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
