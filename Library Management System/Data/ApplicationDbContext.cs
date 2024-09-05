using Library_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Loan> Loans { get; set; } = null!;
        public DbSet<Member> Members { get; set; } = null!;





        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {


            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure shadow properties for Loan entity
            modelBuilder.Entity<Loan>()
                .Property<DateTime>("LoanDate")
                .HasDefaultValueSql("GETDATE()"); // SQL Server, auto-sets date

            modelBuilder.Entity<Loan>()
                .Property<DateTime?>("ReturnDate")
                .IsRequired(false); // ReturnDate can be null

            modelBuilder.Entity<Loan>()
                .Property<DateTime>("DueDate")
                .HasDefaultValueSql("GETDATE()"); // Set default date logic if needed

            base.OnModelCreating(modelBuilder);
        }
    }
}



//// Setting a shadow property
//var loan = new Loan { BookID = 1, MemberID = 2 };
//context.Loans.Add(loan);
//context.Entry(loan).Property("DueDate").CurrentValue = DateTime.Now.AddDays(14); // Set due date
//context.SaveChanges();

//// Accessing a shadow property
//var savedLoan = context.Loans.First();
//var dueDate = context.Entry(savedLoan).Property("DueDate").CurrentValue;
