using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.Core.Models
{
    // Represents a member of the library
    public class Member
    {
        public int Id { get; set; }                 // EF vill kunna sätta
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public DateTime MemberSince { get; set; }

        // Navigation (1 medlem -> många lån)
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();

        // EF behöver parameterlös ctor
        public Member() { }

        // Din "vanliga" ctor kan finnas kvar
        public Member(int id, string name, string email, DateTime memberSince)
        {
            Id = id;
            Name = name;
            Email = email;
            MemberSince = memberSince;
        }

        // Aktiva lån = lån utan ReturnDate
        public List<Loan> GetActiveLoans()
            => Loans.Where(l => !l.IsReturned).ToList();

        // Del 1-kod kan ha anropat Member.AddLoan(...)
        public void AddLoan(Loan loan)
        {
            if (loan == null) return;
            Loans.Add(loan);
        }

        // Del 1-kod kan ha använt Member.BorrowedBooks
        [NotMapped]
           public IReadOnlyList<Book> BorrowedBooks =>
            Loans
                .Where(l => !l.IsReturned)
                .Select(l => l.Book)
                .Where(b => b is not null)
                .Cast<Book>()
                .ToList();

        // Ny "riktiga" metod (bra för tester där du vill styra datum)
        public bool HasOverdueBooks(DateTime today) =>
            Loans.Any(l => l.IsOverdue(today));

        // -----------------------------
        // Kompatibilitet med Del 1/tester
        // -----------------------------

        // Program.cs (Del 1) verkar använda member.GetInfo()
        public string GetInfo()
        {
            return $"{Id}: {Name} ({Email}) - Medlem sedan {MemberSince:yyyy-MM-dd}";
        }

        // Om gammal kod/tester anropar HasOverdueBooks() utan parameter
        public bool HasOverdueBooks() => HasOverdueBooks(DateTime.Today);

        // Om ett test anropar HasOverdueBooks(loan, book) (som din felbild antyder)
        public bool HasOverdueBooks(Loan loan, Book book)
        {
            if (loan == null || book == null) return false;
            if (loan.Book == null) return false;

            // Kolla att lånet gäller den boken
            if (loan.Book.ISBN != book.ISBN) return false;

            return loan.IsOverdue(DateTime.Today);
        }
    }
}