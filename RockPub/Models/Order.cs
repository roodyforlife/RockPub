using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RockPub.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public Place Place { get; set; }
        public int PlaceId { get; set; }
        [Display(Name = "Is completed")]
        [Required(ErrorMessage = "Empty field")]
        public bool IsCompleted { get; set; }
        [Display(Name = "Is paid")]
        [Required(ErrorMessage = "Empty field")]
        public bool IsPaid { get; set; }
        [Required(ErrorMessage = "Empty field")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string Note { get; set; }
        public Staff Staff { get; set; }
        public int StaffId { get; set; }
        public List<DishOrder> DishOrders { get; set; }
    }
}
