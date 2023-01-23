using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RockPub.Models
{
    public class Hall
    {
        public int HallId { get; set; }
        [Display(Name = "Hall name")]
        [Required(ErrorMessage = "Empty field")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string HallName { get; set; }
        public List<Place> Places { get; set; }
    }
}
