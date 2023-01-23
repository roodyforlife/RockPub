using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RockPub.Models
{
    public class Place
    {
        public int PlaceId { get; set; }
        [Display(Name = "Place number")]
        [Required(ErrorMessage = "Empty field")]
        public int PlaceNumber { get; set; }
        public Hall Hall { get; set; }
        public int HallId { get; set; }
        public List<Order> Orders { get; set; }
    }
}
