using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RockPub.Enums
{
    public enum DishSort
    {
        [Display(Name = "Sort by name (A-Z)")]
        NameAsc,
        [Display(Name = "Sort by name (Z-A)")]
        NameDesc,
        [Display(Name = "Sort by cost (Dear first)")]
        CostAsc,
        [Display(Name = "Sort by cost (cheap first)")]
        CostDesc,
        [Display(Name = "Sort by kilocalories (high first)")]
        KilocaloriesAsc,
        [Display(Name = "Sort by kilocalories  (low first)")]
        KilocaloriesDesc,
        [Display(Name = "Sort by weight")]
        WeightAsc,
        [Display(Name = "New first")]
        CreateDateAsc,
        [Display(Name = "Old first")]
        CreateDesc
    }
}
