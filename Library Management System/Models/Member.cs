using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Models
{
    public class Member
    {
        [Key]
        public Guid MemberID { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(12)]
        public string FullName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [Phone]
        [MaxLength(10)]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public DateTime MembershipDate { get; set; }

        public virtual ICollection<Loan> Loans { get; set; } = null!;
    }
}
