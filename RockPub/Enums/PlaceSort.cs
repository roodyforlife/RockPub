using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RockPub.Enums
{
    public enum PlaceSort
    {
        [Display(Name = "Sort by place number (first low)")]
        PlaceNumberAsc,
        [Display(Name = "Sort by place number (first high)")]
        PlaceNumberDesc,
        [Display(Name = "Sort by hall name (A-Z)")]
        HallNameAsc,
        [Display(Name = "Sort by hall name (Z-A)")]
        HallNameDesc
    }
}
