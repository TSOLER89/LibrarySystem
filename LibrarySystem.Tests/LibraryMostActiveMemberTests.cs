using LibrarySystem.Core.Models;
using LibrarySystem.Core.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace LibrarySystem.Tests
{
    public class LibraryMostActiveMemberTests
    {
        [Fact]
        public void Should_Return_Member_With_Most_Loans()
        {
            var member1 = new Member(1, "Alice", "alice@test.com", DateTime.Now);
            var member2 = new Member(2, "Bob", "bob@test.com", DateTime.Now);

            var book1 = new Book("1", "Book 1", "Author A", 2024);
            var book2 = new Book("2", "Book 2", "Author B", 2024);

            var loan1 = new Loan(book1, member1, DateTime.Now.AddDays(-10));
            var loan2 = new Loan(book2, member1, DateTime.Now.AddDays(-4));

            member1.AddLoan(loan1);
            member1.AddLoan(loan2);

            var loans = new List<Loan> { loan1, loan2 };
            var books = new List<Book> { book1, book2 };
            var members = new List<Member> { member1, member2 };

            var bookCatalog = new BookCatalog(books);
            var memberRegistry = new MemberRegistry(members);
            var loanManager = new LoanManager(loans);
            var library = new Library(bookCatalog, memberRegistry, loanManager);

            var result = library.MostActiveMember();

            Assert.Equal(member1, result);
        }
    }
}