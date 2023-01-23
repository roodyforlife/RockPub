using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Empty field")]
        public int Quantity { get; set; }
    }
}
