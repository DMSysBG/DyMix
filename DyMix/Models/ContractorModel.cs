using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DyMix.Models
{
    [Serializable]
    public class ContractorModel
    {
        public int ContractorId { get; set; }

        public int ContractorTypeId { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "ИН по ДДС")]
        public string IdContractor { get; set; }

        public int TownId { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Мол")]
        public string AccountablePerson { get; set; }
    }
}