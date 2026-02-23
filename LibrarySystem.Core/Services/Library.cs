using LibrarySystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrarySystem.Core.Services
{
    public class Library
    {
        private readonly BookCatalog _bookCatalog;
        private readonly MemberRegistry _memberRegistry;
        private readonly LoanManager _loanManager;

        // Konstruktor som tar emot de tre service-klasserna (komposition)
        public Library(BookCatalog bookCatalog, MemberRegistry memberRegistry, LoanManager loanManager)
        {
            _bookCatalog = bookCatalog ?? throw new ArgumentNullException(nameof(bookCatalog));
            _memberRegistry = memberRegistry ?? throw new ArgumentNullException(nameof(memberRegistry));
            _loanManager = loanManager ?? throw new ArgumentNullException(nameof(loanManager));
        }

        // --- Book-relaterade metoder ---
        public int TotalBooks()
        {
            return _bookCatalog.GetAllBooks().Count;
        }

        public List<Book> SearchBooksByTitle(string title)
        {
            return _bookCatalog.SearchByTitle(title);
        }

        public List<Book> GetAllBooksSorted()
        {
            return _bookCatalog.SortByTitle();
        }

        // --- Member-relaterade metoder ---
        public void AddMember(Member member)
        {
            _memberRegistry.AddMember(member);
        }

        public Member FindMember(int memberId)
        {
            return _memberRegistry.FindMemberById(memberId);
        }

        public List<Member> GetMembersWithOverdueBooks()
        {
            return _memberRegistry.GetMembersWithOverdueBooks();
        }

        // --- Loan-relaterade metoder ---
        public Loan BorrowBook(string isbn, int memberId)
        {
            var book = _bookCatalog.FindBookByISBN(isbn);
            var member = _memberRegistry.FindMemberById(memberId);

            return _loanManager.CreateLoan(book, member);
        }

        public void ReturnBook(Loan loan)
        {
            _loanManager.ReturnBook(loan);
        }

        public int BorrowedBooksCount()
        {
            return _loanManager.GetActiveLoans().Count;
        }

        public List<Loan> GetOverdueLoans()
        {
            return _loanManager.GetOverdueLoans();
        }

        public int CalculateTotalLateFees()
        {
            return _loanManager.CalculateTotalLateFees();
        }

        // --- Statistik-metoder ---
        public Member MostActiveMember()
        {
            var allMembers = _memberRegistry.GetAllMembers();

            if (!allMembers.Any())
                throw new InvalidOperationException("No members in the system");

            return allMembers
                .OrderByDescending(m => m.BorrowedBooks.Count)
                .First();
        }

        public string GetLibraryStatistics()
        {
            var totalBooks = TotalBooks();
            var borrowedBooks = BorrowedBooksCount();
            var availableBooks = totalBooks - borrowedBooks;
            var overdueLoans = GetOverdueLoans().Count;
            var totalLateFees = CalculateTotalLateFees();
            var totalMembers = _memberRegistry.GetAllMembers().Count;

            return $@"
=== Library Statistics ===
Total Books: {totalBooks}
Available Books: {availableBooks}
Borrowed Books: {borrowedBooks}
Overdue Loans: {overdueLoans}
Total Late Fees: ${totalLateFees}
Total Members: {totalMembers}
========================";
        }
    }
}