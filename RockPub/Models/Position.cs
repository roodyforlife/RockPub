using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RockPub.Models
{
    public class Position
    {
        public int PositionId { get; set; }
        [Display(Name = "Position name")]
        [Required(ErrorMessage = "Empty field")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string PositionName { get; set; }
        public List<Staff> Staffs { get; set; }
    }
}
