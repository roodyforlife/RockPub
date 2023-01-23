using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPub.Models
{
    public class Dish
    {
        public int DishId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Cost { get; set; }
        public double Kilocalories { get; set; } // Kilocalories per 100 gramm
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Carbohydrates { get; set; }
        public int Weight { get; set; }
        public DateTime CreateDate { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public List<DishOrder> DishOrders { get; set; }
    }
}
