using Xunit;
using LibrarySystem.Core.Models;
using System;

namespace LibrarySystem.Tests;

public class LoanLateFeeTests
{
    [Fact]
    public void CalculateLateFee_Should_Return_Fee_When_Loan_Is_Overdue()
    {
        var book = new Book("1", "1984", "George Orwell", "1949");
        var member = new Member(1, "Alice");

        var loanDate = DateTime.Today.AddDays(-20); // 6 dagar för sent
        var loan = new Loan(book, member, loanDate);

        var fee = loan.CalculateLateFee(DateTime.Today);

        Assert.Equal(30, fee); // 6 * 5 kr
    }
}
