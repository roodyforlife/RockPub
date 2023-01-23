using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RockPub.Models
{
    public class Staff
    {
        public int StaffId { get; set; }
        [Required(ErrorMessage = "Empty field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Empty field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Empty field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string Patronymic { get; set; }
        [Display(Name = "Birthday date")]
        [Required(ErrorMessage = "Empty field")]
        public DateTime BirthdayDate { get; set; }
        [Required(ErrorMessage = "Empty field")]
        public int Salary { get; set; }
        [Required(ErrorMessage = "Empty field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Empty field")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Invalid length")]
        public string Email { get; set; }
        [Display(Name = "Employment date")]
        [Required(ErrorMessage = "Empty field")]
        public DateTime EmploymentDate { get; set; }
        public Position Position { get; set; }
        public int PositionId { get; set; }
        public List<Order> Orders { get; set; }
    }
}
