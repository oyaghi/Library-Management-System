using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Models
{
    public class Book
    {
        [Key]
        public Guid BookID { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(20)]
        public string Title { get; set; } = null!;        [Required]
        [MaxLength(8)]
        public DateTime PublishedDate { get; set; }

        public Guid AuthorID { get; set; }
        [Required]
        [MaxLength(20)]
        public string Genre { get; set; } = null!;
        [Required]
        [MaxLength(1000)]
        [MinLength(100)]
        public int Pages { get; set; }

        public virtual Author Author { get; set; } = null!;
        public virtual ICollection<Loan> Loans { get; set; } = null!;
    }
}
