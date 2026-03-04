using LibrarySystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data.Repository;

public class LoanRepository : ILoanRepository
{
    private readonly LibraryContext _db;

    public LoanRepository(LibraryContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Loan>> GetAllAsync()
    {
        return await _db.Loans
            .Include(l => l.Book)
            .Include(l => l.Member)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Loan>> GetActiveLoansAsync()
    {
        return await _db.Loans
            .Include(l => l.Book)
            .Include(l => l.Member)
            .Where(l => l.ReturnDate == null)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Loan loan)
    {
        _db.Loans.Add(loan);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Loan loan)
    {
        _db.Loans.Update(loan);
        await _db.SaveChangesAsync();
    }

    public Task<Book?> GetBookByIdAsync(int bookId)
        => _db.Books.FirstOrDefaultAsync(b => b.Id == bookId);

    public Task<Member?> GetMemberByIdAsync(int memberId)
        => _db.Members.FirstOrDefaultAsync(m => m.Id == memberId);

    public async Task<Loan?> GetActiveLoanForBookAsync(int bookId)
    {
        return await _db.Loans
            .Include(l => l.Book)
            .Include(l => l.Member)
            .FirstOrDefaultAsync(l => l.BookId == bookId && l.ReturnDate == null);
    }
}