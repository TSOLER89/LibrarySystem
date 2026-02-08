using Xunit;
using System.Collections.Generic;
using System.Text;
using LibrarySystem.Core.Services;

namespace LibrarySystem.Tests
{
    public class LibraryBorrowedBooksTests
    {
        [Fact]
        public void Should_Return_Number_Of_Borrowed_Books()
        {
            var book1 = new Book("Book1", "Author", "1");
            var book2 = new Book("Book2", "Author", "2");

            var member = new Member(1, "Alice");

            var loan = new Loan(book1, member, DateTime.Now.AddDays(-5));

            var books = new List<Book> { book1, book2 };
            var loans = new List<Loan> { loan };

            var library = new Library(books, loans);

            Assert.Equal(1, library.BorrowedBooksCount());        }
    }
}
