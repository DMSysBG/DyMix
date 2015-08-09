using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DyMix.Models
{
    public class ContractorModel
    {
        public int ContractorId { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.ErrorMessages))]
        [Display(Name = "Име")]
        public string Name { get; set; }

        public string IdContractor { get; set; }

        public int TownId { get; set; }

        public string Address { get; set; }

        public string AccountablePerson { get; set; }
    }
}