using Xunit;
using LibrarySystem.Core.Services;
using LibrarySystem.Core.Models;
using System.Collections.Generic;


namespace LibrarySystem.Tests
{
    public class LibraryStatisticsTests
    {
        [Fact]
        // Test to verify that the TotalBooks method returns the correct count of books in the library

        public void Should_Return_Total_Number_Of_Books()
        {
            // Arrange
            var books = new List<Book>
            {
                // Creating two book instances to add to the library
                new Book("1", "A", "A", "2024"),
                new Book("2", "B", "B", "2024"),
            };
            // Creating a library instance with the list of books and an empty list of loans
            var library = new Library(books, new List<Loan>());

            // Act
            Assert.Equal(2, library.TotalBooks());
        }
    }

}
