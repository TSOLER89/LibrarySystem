using LibrarySystem.Core.Models;

namespace LibrarySystem.Data.Repositories;

public interface IBookRepository
{
    // CRUD operations for Book entity //
    // Task betyder “asynkront” (körs utan att låsa appen)
    Task<IEnumerable<Book>> GetAllAsync();

    //Book? betyder “kan vara null” om boken inte finns
    Task<Book?> GetByIdAsync(int id);
    Task<Book?> GetByISBNAsync(string isbn);
    Task AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(int id);
    Task<IEnumerable<Book>> SearchAsync(string searchTerm);
}