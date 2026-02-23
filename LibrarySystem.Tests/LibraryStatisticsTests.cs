using Xunit;
using LibrarySystem.Core.Services;
using LibrarySystem.Core.Models;
using System;
using System.Collections.Generic;

namespace LibrarySystem.Tests
{
    public class LibraryStatisticsTests
    {
        [Fact]
        public void Should_Return_Total_Number_Of_Books()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book("1", "A", "A", 2024),
                new Book("2", "B", "B", 2024),
            };

            var bookCatalog = new BookCatalog(books);
            var memberRegistry = new MemberRegistry();
            var loanManager = new LoanManager();
            var library = new Library(bookCatalog, memberRegistry, loanManager);

            Assert.Equal(2, library.TotalBooks());
        }
    }
}