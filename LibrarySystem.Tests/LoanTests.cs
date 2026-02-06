using Xunit;
using LibrarySystem.Core.Models;

namespace LibrarySystem.Tests
{
    public class LoanTests
    {
        [Fact]
        public void Loan_Should_Be_Overdue_After_14_Days()
        {
            // Arrange
            var book = new Book ("Test", "Author", "1")
            var member = new Member("Alice", 1);
            
            var loan = new LoanTests (book, member, DateTime.Now.AddDays(-15));
            //Assert
            Assert.True(loan.IsOverdue);

        }
    }
}
