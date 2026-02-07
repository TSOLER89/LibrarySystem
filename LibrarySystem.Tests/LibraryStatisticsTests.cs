using Xunit;
using LibrarySystem.Core.Services;
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
                new Book("A", "A", "1"),
                new Book("B", "B", "2"),
            };
            var library = new Library(books, new List<Loan>());

            Assert.Equal(2, library.TotalBooks());
        }
    }

}
