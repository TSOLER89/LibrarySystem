using LibrarySystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.Core.Services
{
    // Service for managing the book catalog
    public class BookCatalog
    {
        private readonly List<Book> _books;
       
        public BookCatalog(List<Book> books)
        {
            _books = books;
        }


        // Search for books by title
        public List<Book> SearchByTitle(string title)
        {
            return _books
                .Where(b => b.Title.Contains(title))
                .ToList();
        }

        // Search for books by author
        public List<Book> SortByTitle()
        {
            return _books
                .OrderBy(b => b.Title)
                .ToList();
        }
    }
}
