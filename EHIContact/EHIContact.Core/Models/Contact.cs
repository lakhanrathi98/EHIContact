using System.ComponentModel.DataAnnotations;

namespace EHIContact.Core.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [StringLength(30)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public bool ActiveStatus { get; set; }        
    }
}
