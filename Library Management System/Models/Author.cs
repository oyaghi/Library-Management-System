using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Models
{
    public class Author
    {
        [Key]
        public Guid AuthorID { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(12)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(8)]
        public DateTime Birthdate { get; set; }
        [MaxLength(150)]
        public string Biography { get; set; } = null!;
        
        public virtual Book Book { get; set; } = null!;
    }
}
