using Xunit;
using LibrarySystem.Core.Models;
using System;

namespace LibrarySystem.Tests
{
    public class LateFeeCalculationTests
    {
        // [Theory] låter oss köra samma test med olika data
        [Theory]
        [InlineData(0, 0)]      // 0 dagar sent = $0
        [InlineData(1, 5)]      // 1 dag sent = $5
        [InlineData(5, 25)]     // 5 dagar sent = $25
        [InlineData(10, 50)]    // 10 dagar sent = $50
        [InlineData(30, 150)]   // 30 dagar sent = $150
        public void Should_Calculate_Correct_Late_Fee_Based_On_Days(int daysOverdue, int expectedFee)
        {
            // Arrange
            var book = new Book("1", "Test Book", "Test Author", 2024);
            var member = new Member(1, "Test Member", "test@test.com", DateTime.Now);

            // Skapa ett lån som är försenat
            var loanDate = DateTime.Now.AddDays(-14 - daysOverdue); // 14 dagar + extra förseningsdagar
            var loan = new Loan(book, member, loanDate);

            // Act
            var actualFee = loan.CalculateLateFee(DateTime.Now);

            // Assert
            Assert.Equal(expectedFee, actualFee);
        }

        [Theory]
        [InlineData("978-0451524935", "1984", "George Orwell", 1949)]
        [InlineData("978-0547928227", "The Hobbit", "J.R.R. Tolkien", 1937)]
        [InlineData("978-0060850524", "Brave New World", "Aldous Huxley", 1932)]
        public void Book_Should_Be_Created_With_Correct_Properties(string isbn, string title, string author, int year)
        {
            // Arrange & Act
            var book = new Book(isbn, title, author, year);

            // Assert
            Assert.Equal(isbn, book.ISBN);
            Assert.Equal(title, book.Title);
            Assert.Equal(author, book.Author);
            Assert.Equal(year, book.PublishedYear);
            Assert.True(book.IsAvailable);
        }
    }
}