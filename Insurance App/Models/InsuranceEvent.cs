using System.ComponentModel.DataAnnotations;

namespace PojisteniApp.Models
{
    public class InsuranceEvent
    {
        [Key]
        public int EventId { get; set; }

        [Display(Name = "Název události")]
        [Required(ErrorMessage = "Název je nutný")]
        [StringLength(maximumLength: 60)]
        public string EventName { get; set; } = "";

        public string EventDescription { get; set; } = "";

        [Required(ErrorMessage = "ID je nutné")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a personId.")]
        public int PersonId { get; set; }

        public InsuracePersonData? Person { get; set; }
    }
}
