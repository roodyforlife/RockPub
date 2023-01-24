using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RockPub.Models
{
    public class Dish
    {
        public int DishId { get; set; }
        [Required(ErrorMessage = "Empty field")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Empty field")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Empty field")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Empty field")]
        public int Cost { get; set; }
        [Display(Name = "Kilocalories per 100 gramm")]
        [Required(ErrorMessage = "Empty field")]
        public double Kilocalories { get; set; }
        [Display(Name = "Protein per 100 gramm")]
        [Required(ErrorMessage = "Empty field")]
        public double Protein { get; set; }
        [Display(Name = "Fat per 100 gramm")]
        [Required(ErrorMessage = "Empty field")]
        public double Fat { get; set; }
        [Display(Name = "Carbohydrates per 100 gramm")]
        [Required(ErrorMessage = "Empty field")]
        public double Carbohydrates { get; set; }
        [Display(Name = "Weight (gramm)")]
        [Required(ErrorMessage = "Empty field")]
        public int Weight { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public List<DishOrder> DishOrders { get; set; }
    }
}
