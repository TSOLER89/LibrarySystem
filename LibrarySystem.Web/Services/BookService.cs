using LibrarySystem.Core.Models;
using LibrarySystem.Data.Repository;

namespace LibrarySystem.Web.Services;

public class BookService
{
    private readonly IBookRepository _repo;

    public BookService(IBookRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<Book>> GetAllAsync() => _repo.GetAllAsync();

    public Task<IEnumerable<Book>> SearchAsync(string term) => _repo.SearchAsync(term);

    public Task<Book?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    public Task AddAsync(Book book) => _repo.AddAsync(book);

    public Task UpdateAsync(Book book) => _repo.UpdateAsync(book);

    public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
}