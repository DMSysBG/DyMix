using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DyMix.Models
{
    public class DiscountModel
    {
        public int DiscountId { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Активна от")]
        public DateTime ValidFrom { get; set; }

        [Display(Name = "Активна до")]
        public DateTime ValidTo { get; set; }

        [Display(Name = "Важи за")]
        public int DiscountKindId { get; set; }

        public string DiscountKindName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Стойност")]
        public decimal Value { get; set; }
    }


    public class DiscountGroupModel
    {
        public int DiscountGroupId { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Display(Name = "Отстъпки")]
        public string DiscountList { get; set; }
    }

    public class DiscountKindModel
    {
        public int DiscountKindId { get; set; }

        public string Name { get; set; }
    }
}