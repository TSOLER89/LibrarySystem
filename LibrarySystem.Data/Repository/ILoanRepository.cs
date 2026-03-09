using LibrarySystem.Core.Models;

namespace LibrarySystem.Data.Repository;

public interface ILoanRepository
{
    Task<IEnumerable<Loan>> GetActiveLoansAsync();
    Task<IEnumerable<Loan>> GetAllAsync();

    Task AddAsync(Loan loan);
    Task UpdateAsync(Loan loan);

    Task<Book?> GetBookByIdAsync(int bookId);
    Task<Member?> GetMemberByIdAsync(int memberId);

    Task<Loan?> GetActiveLoanForBookAsync(int bookId);
}