using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RockPub.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Empty field")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string Name { get; set; }
        public List<Dish> Dishes { get; set; }
    }
}
