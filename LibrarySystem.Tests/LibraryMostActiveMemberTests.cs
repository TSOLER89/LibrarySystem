using LibrarySystem.Core.Models;
using System;
using System.Collections.Generic;
using Xunit;


namespace LibrarySystem.Tests
{
    public class LibraryMostActiveMemberTests
    {
        [Fact]
        // Test method to verify that the most active member is correctly identified
        public void Should_Return_Member_With_Most_Loans()
        {
            var member1 = new Member(1, "Alice");
            var member2 = new Member(2, "Bob");

            var book1 = new Book(1, "Book 1", "Author A");
            var book2 = new Book(2, "Book 2", "Author B");

            var loan1 = new Loan(member1, book1, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-5));
            var loan2 = new Loan(member1, book2, DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-1));

            var loans = new List<Loan> { loan1, loan2 };
            var books = new List<Book> { book1, book2 };

            var library = new Library(books, loans);

            var result = library.GetMostActiveMember();

            Assert.Equal(member1, result);
        }

    }
}
