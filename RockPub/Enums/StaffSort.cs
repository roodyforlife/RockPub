using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RockPub.Enums
{
    public enum StaffSort
    {
        [Display(Name = "Sort by name (A-Z)")]
        NameAsc,
        [Display(Name = "Sort by name (Z-A)")]
        NameDesc,
        [Display(Name = "Sort by surname (A-Z)")]
        SurnameAsc,
        [Display(Name = "Sort by surname (Z-A)")]
        SurnameDesc,
        [Display(Name = "Sort by patronymic (A-Z)")]
        PatronymicAsc,
        [Display(Name = "Sort by patronymic (Z-A)")]
        PatronymicDesc,
        [Display(Name = "Sort by salary")]
        SalaryAsc,
        [Display(Name = "Sort by birthday date")]
        BirthdayDateAsc,
        [Display(Name = "Sort by email (A-Z)")]
        EmailAsc,
        [Display(Name = "Sort by email (Z-A)")]
        EmailDesc
    }
}
