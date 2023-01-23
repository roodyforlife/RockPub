using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPub.Models
{
    public class Position
    {
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public List<Staff> Staffs { get; set; }
    }
}
