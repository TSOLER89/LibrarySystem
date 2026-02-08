using LibrarySystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LibrarySystem.Core.Services
{

    /// Represents a library that manages books and loans.
    public class Library
    {
        private readonly List<Book> _books;
        private readonly List<Loan> _loans;

        public Library(List<Book> books, List<Loan> loans)
        {
            _books = books;
            _loans = loans;
        }

        /// Returns the total number of books in the library.
        
        public int TotalBooks()
        {
            return _books.Count;
        }


        /// Returns the most active member based on the number of loans.
        public Member MostActiveMember()
        {
            return _loans
                .GroupBy(l => l.Member)
                .OrderByDescending(g => g.Count())
                .First()
                .Key;
        }

        public int BorrowedBooksCount()
        {
            return _loans.Count;
        }
    }
}
