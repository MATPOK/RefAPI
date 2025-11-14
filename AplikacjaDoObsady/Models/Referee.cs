using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplikacjaDoObsady.Models
{
    public class Referee
    {
        [Key]
        public int RefereeId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string League { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }
        
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
    }
}
