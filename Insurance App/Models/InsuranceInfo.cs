using System.ComponentModel.DataAnnotations;

namespace PojisteniApp.Models
{
    public class InsuranceInfo
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Popis")]
        [Required(ErrorMessage = "Popis je nutný")]
        [StringLength(maximumLength: 100)]
        public string DescriptionOfInsurance { get; set; } = "";

        public int DescriptionId { get; set; }
    }
}
