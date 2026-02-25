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

        // Hjälpmetod (din nya modell: aktiva lån = lån utan ReturnDate)
        public List<Loan> GetActiveLoans()
            => Loans.Where(l => !l.IsReturned).ToList();


        // Del 1-kod kan ha anropat Member.AddLoan(...)
        public void AddLoan(Loan loan)
        {
            Loans.Add(loan);
        }

        // Del 1-kod kan ha använt Member.BorrowedBooks
        public IReadOnlyList<Book> BorrowedBooks =>
            Loans
                .Where(l => !l.IsReturned)
                .Select(l => l.Book)
                .ToList();

        // Del 1-kod kan ha använt Member.HasOverdueBooks()
        public bool HasOverdueBooks(DateTime today) =>
            Loans.Any(l => l.IsOverdue(today));
    }
}