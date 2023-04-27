using System.ComponentModel.DataAnnotations;

namespace PojisteniApp.Models
{
    public class InsuracePersonData
    {
        [Key]
        public int PersonId { get; set; }

        [Display(Name = "Jméno")]
        [Required(ErrorMessage = "Jméno je nutné")]
        [StringLength(maximumLength: 60)]
        public string FirstName { get; set; } = "";

        [Display(Name = "Příjmení")]
        [Required(ErrorMessage = "Jméno je nutné")]
        [StringLength(maximumLength: 60)]
        public string LastName { get; set; } = "";


        [Display(Name = "Rodné číslo")]
        [Required(ErrorMessage = "Rodné číslo je nutné")]
        [RegularExpression(@"^([0-9]{6})$", ErrorMessage = "Social security number is invalid.")]
        public int SocialNumber { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email je nutné")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email není ve správném formátu.")]
        public string Email { get; set; } = "";

        [Display(Name = "Telefon")]
        [Required(ErrorMessage = "Telefon je nutné")]
        [RegularExpression(@"^(\+?)([0-9]{3})?([0-9]{9})$", ErrorMessage = "Telefon není ve správném formátu.")]
        public string PhoneNumber { get; set; } = "";

        [Display(Name = "Bydliště")]
        [Required(ErrorMessage = "Bydliště je nutné")]
        [StringLength(maximumLength: 100)]
        public string Address { get; set; } = "";

        [Display(Name = "Město")]
        [Required(ErrorMessage = "Město je nutné")]
        [StringLength(maximumLength: 60)]
        public string City { get; set; } = "";

        [Display(Name = "PSČ")]
        [Required(ErrorMessage = "PSČ je nutné")]
        [StringLength(5)]
        [RegularExpression("^[0-9]{3} ?[0-9]{2}$", ErrorMessage = "PSČ není ve správném formátu.")]
        public string PostZipCode { get; set; } = "";


        public virtual ICollection<PersonInsurance>? PersonInsurances { get; set; }
        public virtual ICollection<Insurance>? Insurances { get; set; }

        public virtual ICollection<InsuranceEvent>? InsuranceEvents { get; set; }
    }
}
