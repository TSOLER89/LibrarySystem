using LibrarySystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.Core.Services
{
    public class BookCatalog
    {
        private readonly List<Book> _books;

        public BookCatalog()
        {
            _books = new List<Book>();
        }

        public BookCatalog(List<Book> books)
        {
            _books = books ?? new List<Book>();
        }

        // Lägg till en bok
        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book), "Book cannot be null");

            if (_books.Any(b => b.ISBN == book.ISBN))
                throw new InvalidOperationException($"Book with ISBN {book.ISBN} already exists");

            _books.Add(book);
        }

        // Hitta bok via ISBN
        public Book FindBookByISBN(string isbn)
        {
            if (string.IsNullOrEmpty(isbn))
                throw new ArgumentException("ISBN cannot be null or empty", nameof(isbn));

            var book = _books.FirstOrDefault(b => b.ISBN == isbn);
            if (book == null)
                throw new KeyNotFoundException($"Book with ISBN {isbn} not found");

            return book;
        }

        // Sök böcker via titel
        public List<Book> SearchByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                return new List<Book>();

            return _books
                .Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Sortera böcker alfabetiskt
        public List<Book> SortByTitle()
        {
            return _books
                .OrderBy(b => b.Title)
                .ToList();
        }

        // Få alla böcker
        public List<Book> GetAllBooks()
        {
            return _books.ToList();
        }

        // Få alla tillgängliga böcker
        public List<Book> GetAvailableBooks()
        {
            return _books.Where(b => b.IsAvailable).ToList();
        }

        // Få alla utlånade böcker
        public List<Book> GetBorrowedBooks()
        {
            return _books.Where(b => !b.IsAvailable).ToList();
        }
    }
}