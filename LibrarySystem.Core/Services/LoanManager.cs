using LibrarySystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.Core.Services
{
    public class LoanManager
    {
        private readonly List<Loan> _loans;

        public LoanManager()
        {
            _loans = new List<Loan>();
        }

        public LoanManager(List<Loan> loans)
        {
            _loans = loans ?? new List<Loan>();
        }

        // Skapa ett nytt lån
        public Loan CreateLoan(Book book, Member member)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book), "Book cannot be null");

            if (member == null)
                throw new ArgumentNullException(nameof(member), "Member cannot be null");

            if (!book.IsAvailable)
                throw new InvalidOperationException($"Book '{book.Title}' is not available for loan");

            var loan = new Loan(book, member, DateTime.Now);

            _loans.Add(loan);

            book.MarkAsBorrowed();
            member.AddLoan(loan);

            return loan;
        }

        // Returnera en bok
        public void ReturnBook(Loan loan)
        {
            if (loan == null)
                throw new ArgumentNullException(nameof(loan), "Loan cannot be null");

            if (loan.IsReturned)
                throw new InvalidOperationException("This book has already been returned");

            loan.MarkAsReturned(DateTime.Now);
            loan.Book.MarkAsReturned();
        }

        // Få alla aktiva lån
        public List<Loan> GetActiveLoans()
        {
            return _loans.Where(l => !l.IsReturned).ToList();
        }

        // Få alla försenade lån
        public List<Loan> GetOverdueLoans()
        {
            // ✅ FIX: IsOverdue kräver DateTime today
            return _loans.Where(l => l.IsOverdue(DateTime.Today)).ToList();
        }

        // Få alla lån för en specifik medlem
        public List<Loan> GetLoansByMember(Member member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member), "Member cannot be null");

            return _loans.Where(l => l.Member.Id == member.Id).ToList();
        }

        // Få alla lån för en specifik bok
        public List<Loan> GetLoansByBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book), "Book cannot be null");

            return _loans.Where(l => l.Book.ISBN == book.ISBN).ToList();
        }

        // Räkna totala antalet lån
        public int GetTotalLoansCount()
        {
            return _loans.Count;
        }

        // Beräkna totala förseningsavgifter för alla lån
        public int CalculateTotalLateFees()
        {
            return _loans.Sum(l => l.CalculateLateFee(DateTime.Now));
        }
    }
}