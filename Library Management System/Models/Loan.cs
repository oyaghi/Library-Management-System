using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Models
{
    public class Loan
    {
        [Key]
        public Guid LoanID { get; set; }
        public Guid BookID { get; set; }
        public Guid MemberID { get; set; }
        //public DateTime LoanDate { get; set; }
        //public DateTime? ReturnDate { get; set; }  // Shadow attributes defined in DbContext 
        //public DateTime DueDate { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;
    }
}
