using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PojisteniApp.Models
{
    public class Insurance
    {
        [Key]
        public int InsuranceId { get; set; }

        [Display(Name = "Pojištění")]
        [Required(ErrorMessage = "Pojištění je nutné")]
        public string Name { get; set; } = "";

        [Display(Name = "Popis")]
        [Required(ErrorMessage = "Popis je nutný")]
        [StringLength(maximumLength: 100)]
        public string DescriptionOfInsurance { get; set; } = "";


        [Required(ErrorMessage = "ID je nutné")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a personId.")]
        public int PersonId { get; set; }

        public int DescriptionId { get; set; }

        public InsuracePersonData? Person { get; set; }

        [NotMapped]
        public List<SelectListItem>? TypeOfInsurance { get; set; }
    }
}
