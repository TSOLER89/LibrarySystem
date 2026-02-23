using Xunit;
using LibrarySystem.Core.Models;
using System;

namespace LibrarySystem.Tests
{
    public class LoanTests
    {
        [Fact]
        public void Loan_Should_Be_Overdue_After_14_Days()
        {
            // Arrange
            var book = new Book("1", "Test", "Author", 2024);
            var member = new Member(1, "Alice", "alice@test.com", DateTime.Now);

            var loan = new Loan(book, member, DateTime.Now.AddDays(-15));

            //Assert
            Assert.True(loan.IsOverdue());
        }
    }
}