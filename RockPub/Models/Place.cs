using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPub.Models
{
    public class Place
    {
        public int PlaceId { get; set; }
        public int PlaceNumber { get; set; }
        public Hall Hall { get; set; }
        public List<Order> Orders { get; set; }
    }
}
