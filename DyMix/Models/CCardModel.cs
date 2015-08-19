using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DyMix.Models
{
    public class CCardModel
    {
        public int CardId { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Номер")]        
        public string CNumber { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Активна от")]
        public DateTime ValidFrom { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Активна до")]
        public DateTime ValidTo { get; set; }

        [Display(Name = "Блокирана")]
        public bool IsBlocked { get; set; }

        public int? ContractorId { get; set; }

        [Display(Name = "Клиент")]
        public string ContractorName { get; set; }

        public int? DiscountGroupId { get; set; }
    }
}