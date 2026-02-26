using System;
using System.Linq;
using System.Threading.Tasks;
using LibrarySystem.Core.Models;
using LibrarySystem.Core.Services;
using LibrarySystem.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LibrarySystem.Tests.Del2;

public class LoanIntegrationTests
{
    private static LibraryContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new LibraryContext(options);
    }

    [Fact]

    // This test verifies that when a loan is created,
    // the associated book becomes unavailable and the loan is saved in the database.
    public async Task CreateLoan_ShouldMakeBookUnavailable_And_SaveLoan()
    {
        using var context = CreateContext();

        var book = new Book { ISBN = "10", Title = "LoanBook", Author = "A", PublishedYear = 2020, IsAvailable = true };
        var member = new Member { Name = "Alice", Email = "alice@test.com", MemberSince = DateTime.Today };

        context.Books.Add(book);
        context.Members.Add(member);
        await context.SaveChangesAsync();

        var manager = new LoanManager();

        // Act
        var loan = manager.CreateLoan(book, member);

        context.Loans.Add(loan);
        await context.SaveChangesAsync();

        // Assert: book became unavailable + loan exists in DB
        Assert.False(book.IsAvailable);
        Assert.True(await context.Loans.AnyAsync(l => l.BookId == book.Id && l.MemberId == member.Id));
    }

    [Fact]
    // This test verifies that when a loan is returned,
    // the associated book becomes available again and the loan is marked as returned.
    public async Task ReturnBook_ShouldMarkLoanReturned_And_MakeBookAvailable()
    {
        using var context = CreateContext();

        var book = new Book { ISBN = "11", Title = "ReturnBook", Author = "A", PublishedYear = 2020, IsAvailable = true };
        var member = new Member { Name = "Bob", Email = "bob@test.com", MemberSince = DateTime.Today };

        context.Books.Add(book);
        context.Members.Add(member);
        await context.SaveChangesAsync();

        var manager = new LoanManager();
        var loan = manager.CreateLoan(book, member);

        context.Loans.Add(loan);
        await context.SaveChangesAsync();

        // Act
        manager.ReturnBook(loan);
        await context.SaveChangesAsync();

        // Assert
        Assert.True(loan.IsReturned);
        Assert.True(book.IsAvailable);
    }
}
