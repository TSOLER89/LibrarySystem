using LibrarySystem.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Data.Repository
{
	public class BookRepository : IBookRepository
	{
		private readonly LibraryContext _db;

		public BookRepository(LibraryContext db)
		{
			_db = db;
		}


		public async Task<IEnumerable<Book>> GetAllAsync()
		{
			return await _db.Books
				.AsNoTracking()
				.ToListAsync();
		}


		public async Task<Book?> GetByIdAsync(int id)
		{
			return await _db.Books
				.Include(b => b.Loans)
					.ThenInclude(l => l.Member)
				.FirstOrDefaultAsync(b => b.Id == id);
		}


		public async Task<Book?> GetByISBNAsync(string isbn)
		{
			return await _db.Books
				.FirstOrDefaultAsync(b => b.ISBN == isbn);
		}

		public async Task AddAsync(Book book)
		{
			_db.Books.Add(book);
			await _db.SaveChangesAsync();
		}

		public async Task UpdateAsync(Book book)
		{
			_db.Books.Update(book);
			await _db.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var book = await _db.Books.FindAsync(id);
			if (book is null) return;

			_db.Books.Remove(book);
			await _db.SaveChangesAsync(); //SaveChangesAsync() sparar ändringar i DB.
		}


        // gör sökningen case insensitive, via titel, via författare, via ISBN
        public async Task<IEnumerable<Book>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();

            searchTerm = searchTerm
                .Trim()
                .Replace("-", "")
                .Replace(" ", "")
                .ToLower();

            return await _db.Books
                .AsNoTracking()
                .Where(b =>
                    b.Title.ToLower().Contains(searchTerm) ||
                    b.Author.ToLower().Contains(searchTerm) ||
                    b.ISBN.Replace("-", "").Contains(searchTerm) ||
                    b.PublishedYear.ToString().Contains(searchTerm))
                .ToListAsync();
        }
    }
}
	