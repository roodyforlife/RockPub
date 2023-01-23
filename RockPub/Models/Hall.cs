using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPub.Models
{
    public class Hall
    {
        public int HallId { get; set; }
        public string HallName { get; set; }
        public List<Place> Places { get; set; }
    }
}
