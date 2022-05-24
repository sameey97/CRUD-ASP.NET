using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PatientApp.Models
{
    public class PatientModelView
    {
        [Required]

        public int Id { get; set; }

        [Required(ErrorMessage = "Patient name is required")]
        [Display(Name = "Patient Name")]
        public string Patient_Name { get; set; }

        [Required(ErrorMessage = "Patient phone number is required")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not valid phone number")]
        [Display(Name = "Phone Number")]
        public string Patient_Number { get; set; }

        [Required(ErrorMessage = "Patient phone number is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Patient Email")]
        public string Patient_Email { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [StringLength(250)]
        [Display(Name = "Blood Group")]
        public string Blood_Group { get; set; }
    }
}