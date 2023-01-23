using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPub.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public Place Place { get; set; }
        public int PlaceId { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsPaid { get; set; }
        public string Note { get; set; }
        public Staff Staff { get; set; }
        public int StaffId { get; set; }
        public List<DishOrder> DishOrders { get; set; }
    }
}
