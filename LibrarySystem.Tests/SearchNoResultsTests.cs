using Xunit;
using LibrarySystem.Core.Models;
using LibrarySystem.Core.Services;
using System.Collections.Generic;

namespace LibrarySystem.Tests
{
    public class SearchNoResultsTests
    {
        [Fact]
        public void Search_Should_Return_Empty_List_When_No_Match_Found()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book("1", "1984", "George Orwell", 1949),
                new Book("2", "The Hobbit", "J.R.R. Tolkien", 1937)
            };

            var catalog = new BookCatalog(books);

            // Act
            var result = catalog.SearchByTitle("NonExistingBook");

            // Assert
            Assert.Empty(result);
        }
    }
}