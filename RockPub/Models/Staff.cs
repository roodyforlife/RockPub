using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPub.Models
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthdayDate { get; set; }
        public int Salary { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime EmploymentDate { get; set; }
        public Position Position { get; set; }
        public int PositionId { get; set; }
        public List<Order> Orders { get; set; }
    }
}
