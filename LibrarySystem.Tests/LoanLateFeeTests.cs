using Xunit;
using LibrarySystem.Core.Models;
using System;

namespace LibrarySystem.Tests
{
    public class LoanLateFeeTests
    {
        [Fact]
        public void Should_Calculate_Late_Fee_Correctly()
        {
            // Arrange
            var book = new Book("1", "Test Book", "Test Author", 2024);
            var member = new Member(1, "Alice", "alice@test.com", DateTime.Now);

            // Skapa ett lån som är 5 dagar försenat
            var loanDate = DateTime.Now.AddDays(-19); // 14 + 5 dagar
            var loan = new Loan(book, member, loanDate);

            // Act
            var fee = loan.CalculateLateFee(DateTime.Now);

            // Assert
            Assert.Equal(25, fee); // 5 dagar * $5 = $25
        }
    }
}