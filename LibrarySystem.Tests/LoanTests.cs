using Xunit;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Tests
{
    public class LoanTests
    {
        [Fact]
        // Loan should be overdue after 14 days
        public void Loan_Should_Be_Overdue_After_14_Days()
        {
            // Arrange
            var book = new Book("1", "Test", "Author", "2024");
            var member = new Member(1, "Alice");

            var loan = new Loan(book, member, DateTime.Now.AddDays(-15));
            //Assert
            Assert.True(loan.IsOverdue());

        }
    }
}

