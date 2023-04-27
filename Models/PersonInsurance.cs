using System.ComponentModel.DataAnnotations;

namespace PojisteniApp.Models
{
    public class PersonInsurance
    {
        [Key]
        public int PersonInsuranceId { get; set; }

        [Display(Name = "Částka")]
        [Required(ErrorMessage = "Částka je nutná")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only numbers are allowed.")]
        public int ValueOfInsurance { get; set; }

        [Display(Name = "Předmět pojištění")]
        [Required(ErrorMessage = "Nutné")]
        [RegularExpression(@"^[a-zA-Zá-žÁ-Ž]+$", ErrorMessage = "Only letters are allowed.")]
        public string IntrestOfInsurance { get; set; } = "";

        [Display(Name = "Datum od")]
        [Required(ErrorMessage = "Nutné")]
        //[RegularExpression(@"^([0-9]{2})\/([0-9]{2})\/([0-9]{4})$", ErrorMessage = "Date is invalid.")]
        public DateTime InsuranceStart { get; set; }


        [Display(Name = "Datum do")]
        [Required(ErrorMessage = "Nutné")]
        //[RegularExpression(@"^([0-9]{2})\/([0-9]{2})\/([0-9]{4})$", ErrorMessage = "Date is invalid.")]
        public DateTime InsuranceEnd { get; set; }


        [Display(Name = "Rodné číslo")]
        [Required(ErrorMessage = "RČ je nutné")]
        public int PersonId { get; set; }
        public InsuracePersonData? Person { get; set; }
    }
}
