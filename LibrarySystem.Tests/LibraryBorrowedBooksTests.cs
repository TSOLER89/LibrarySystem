using Xunit;
using System;
using System.Collections.Generic;
using System.Text;
using LibrarySystem.Core.Models;
using LibrarySystem.Core.Services;

namespace LibrarySystem.Tests
{
    public class LibraryBorrowedBooksTests
    {
        [Fact]
        public void Should_Return_Number_Of_Borrowed_Books()
        {
            var book1 = new Book("1", "Book1", "Author", 2024);
            var book2 = new Book("2", "Book2", "Author", 2024);

            var member = new Member(1, "Alice", "alice@test.com", DateTime.Now);

            var loan = new Loan(book1, member, DateTime.Now.AddDays(-5));

            var books = new List<Book> { book1, book2 };
            var loans = new List<Loan> { loan };

            var bookCatalog = new BookCatalog(books);
            var memberRegistry = new MemberRegistry();
            var loanManager = new LoanManager(loans);
            var library = new Library(bookCatalog, memberRegistry, loanManager);

            Assert.Equal(1, library.BorrowedBooksCount());
        }
    }
}