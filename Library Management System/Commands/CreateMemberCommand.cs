using Library_Management_System.Models;
using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Commands
{
    public class CreateMemberCommand
    {
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
    }
}
