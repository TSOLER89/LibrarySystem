using Xunit;
using LibrarySystem.Core.Models;
using System;

namespace LibrarySystem.Tests;

public class LoanLateFeeTests
{
    [Fact]

    // Test to verify that the CalculateLateFee method returns the correct fee when a loan is overdue
    public void CalculateLateFee_Should_Return_Fee_When_Loan_Is_Overdue()
    {
        // Arrange
        var book = new Book("1", "1984", "George Orwell", "1949");
        var member = new Member(1, "Alice");

        // Simulate a loan that was made 20 days ago, which is 6 days overdue (assuming a 14-day loan period)
        var loanDate = DateTime.Today.AddDays(-20); // 6 dagar för sent
        var loan = new Loan(book, member, loanDate);

        var fee = loan.CalculateLateFee(DateTime.Today);

        Assert.Equal(30, fee); // 6 * 5 kr
    }
}
