using Xunit;
using LibrarySystem.Core.Models;
using System;

namespace LibrarySystem.Tests
{
    public class MemberTests
    {
        [Fact]
        public void Member_Should_Be_Created_With_Empty_Borrowed_Books_List()
        {
            // Arrange & Act
            var member = new Member(1, "Alice", "alice@test.com", DateTime.Now);

            // Assert
            Assert.Empty(member.BorrowedBooks);
        }

        [Fact]
        public void Member_Should_Track_Borrowed_Books()
        {
            // Arrange
            var member = new Member(1, "Alice", "alice@test.com", DateTime.Now);
            var book = new Book("1", "Test Book", "Author", 2024);
            var loan = new Loan(book, member, DateTime.Now);

            // Act
            member.AddLoan(loan);

            // Assert
            Assert.Single(member.BorrowedBooks);
            Assert.Equal(loan, member.BorrowedBooks[0]);
        }

        [Fact]
        public void Member_Should_Detect_Overdue_Books()
        {
            // Arrange
            var member = new Member(1, "Alice", "alice@test.com", DateTime.Now);
            var book = new Book("1", "Test Book", "Author", 2024);
            var overdueLoan = new Loan(book, member, DateTime.Now.AddDays(-20));
            member.AddLoan(overdueLoan);

            // Act & Assert
            Assert.True(member.HasOverdueBooks());
        }
    }
}