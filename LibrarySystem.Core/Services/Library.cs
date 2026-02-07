using LibrarySystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Core.Services
{
    public class Library
    {
        private readonly List<Book> _books;
        private readonly List<Loan> _loans;

        public Library(List<Book> books, List<Loan> loans)
        {
            _books = books;
            _loans = loans;
        }

        public int TotalBooks()
        {
            return _books.Count;
        }

        public Member MostActiveMember()
        {
            return _loans
                .GroupBy(l => l.Member)
                .OrderByDescending(g => g.Count())
                .First()
                .Key;
        }
    }
}
