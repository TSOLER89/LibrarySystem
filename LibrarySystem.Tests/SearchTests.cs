using Xunit;
using System.Collections.Generic;
using System.Text;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Tests
{
    public class SearchTests
    {

        //book should match search by title or author
        [Fact]
        public void Book_Should_Match_Search_By_Title_Or_Author()
        {
            // Arrange
            var book = new Book("1", "1984", "George Orwell", "1949");

            // Act & Assert
            Assert.True(book.Matches("1984"));
            Assert.True(book.Matches("George Orwell"));
            Assert.False(book.Matches("Brave New World"));


        }


        //Book should match search by ISBN
        [Fact]
        public void Book_Should_Match_Search_By_Isbn()
        {
            // Arrange
            var book = new Book(
                "978-91-0-012345-6",
                "Sagan om ringen",
                "J.R.R. Tolkien",
                "1954");

            // Act
            var result = book.Matches("978-91-0-012345-6");

            // Assert
            Assert.True(result);
        }
    }
}

