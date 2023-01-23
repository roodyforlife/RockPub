﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPub.Models
{
    public class DishOrder
    {
        public int DishOrderId { get; set; }
        public Dish Dish { get; set; }
        public int DishId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
    }
}
