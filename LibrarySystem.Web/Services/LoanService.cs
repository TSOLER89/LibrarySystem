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


    // Skapa ett lån: val av bok och medlem, kontrollera att boken är tillgänglig, skapa lånet och uppdatera bokstatus
    public async Task CreateLoanAsync(int bookId, int memberId)
    {
        if (bookId <= 0) throw new ArgumentException("Välj en bok.", nameof(bookId));
        if (memberId <= 0) throw new ArgumentException("Välj en medlem.", nameof(memberId));

        // Hämta entiteter TRACKADE i samma DbContext
        var book = await _repo.GetBookByIdAsync(bookId);
        if (book is null) throw new InvalidOperationException("Boken hittades inte.");

        if (!book.IsAvailable)
            throw new InvalidOperationException("Boken är redan utlånad.");

        var member = await _repo.GetMemberByIdAsync(memberId);
        if (member is null) throw new InvalidOperationException("Medlemmen hittades inte.");

        // Skapa lånet och sätt FK (BookId/MemberId) explicit
        var loan = new Loan
        {
            BookId = bookId,
            MemberId = memberId,
            LoanDate = DateTime.Now,
            DueDate = DateTime.Now.AddDays(14)
        };

        // Uppdatera bokstatus
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