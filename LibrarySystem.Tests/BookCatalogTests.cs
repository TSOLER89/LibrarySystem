using LibrarySystem.Core.Models;
using LibrarySystem.Core.Services;
using System.Collections.Generic;
using Xunit;

namespace LibrarySystem.Tests
{
    public class BookCatalogTests
    {
        [Fact]
        public void SearchByTitle_Return_Matching_Books()
        {
            // Arrange
            var Books = new List<Book>
            {
                new Book("1", "1984", "George Orwell", 1949),
                new Book("2", "Brave New World", "Aldous Huxley", 1932)
            };

            //Act
            var catalog = new BookCatalog(Books);
            var result = catalog.SearchByTitle("1984");

            //Assert
            Assert.Single(result);
            Assert.Equal("1984", result[0].Title);
        }
    }
}

