using LibrarySystem.Core.Models;
using System.Collections.Generic;
using Xunit;

namespace LibrarySystem.Tests
{
    public class BookCatalogTests
    {

        [Fact]
        // Test method to verify searching books by title
        public void SearchByTitle_Return_Matching_Books()
        {
            // Arrange
            var Books = new List<Book>
            {
            new Book("1984", "George Orwell", "1"),
            new Book("Brave New World", "Aldous Huxley", "2")
            };
            //Act

            var catalog = new BookCatalog(Books);

            var result = new BookCatalogTests(Books);
            
            
            //Assert
            Assert.Single(result);
            Assert.Equal("1984", result[0].Title);
        }
    }
}

