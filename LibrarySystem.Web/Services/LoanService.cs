using LibrarySystem.Core.Models;
using LibrarySystem.Data.Repository;

namespace LibrarySystem.Web.Services;

public class LoanService
{
    private readonly ILoanRepository _repo;

    public LoanService(ILoanRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<Loan>> GetActiveLoansAsync()
        => _repo.GetActiveLoansAsync();

    public async Task CreateLoanAsync(int bookId, int memberId)
    {
        var book = await _repo.GetBookByIdAsync(bookId)
            ?? throw new InvalidOperationException("Book not found");

        var member = await _repo.GetMemberByIdAsync(memberId)
            ?? throw new InvalidOperationException("Member not found");

        if (!book.IsAvailable)
            throw new InvalidOperationException("Book is not available");

        // skapa lån
        var loan = new Loan
        {
            BookId = book.Id,
            MemberId = member.Id,
            LoanDate = DateTime.Now,
            DueDate = DateTime.Now.AddDays(14),
            ReturnDate = null
        };

        // markera bok
        book.IsAvailable = false;

        await _repo.AddAsync(loan);
    }

    public async Task ReturnBookAsync(int bookId)
    {
        var active = await _repo.GetActiveLoanForBookAsync(bookId)
            ?? throw new InvalidOperationException("No active loan found for this book");

        active.ReturnDate = DateTime.Now;

        // markera bok tillgänglig
        active.Book.IsAvailable = true;

        await _repo.UpdateAsync(active);
    }
}